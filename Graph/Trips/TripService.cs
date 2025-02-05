using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Pgvector.EntityFrameworkCore;
using Sukalibur.Graph.Notifications;
using Sukalibur.Graph.Organizers;
using Sukalibur.Shared.Services;
using System.Text.Json;

namespace Sukalibur.Graph.Trips
{
    public class TripService : IAsyncDisposable
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly EmbeddingService _embeddingService;
        public TripService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper, IBackgroundJobClient backgroundJobClient, EmbeddingService embeddingService)
        {
            _context = contextFactory.CreateDbContext();
            _mapper = mapper;
            _backgroundJobClient = backgroundJobClient;
            _embeddingService = embeddingService;
        }

        public async Task<Trip> CreateTripAsync(CreateTripInput input)
        {
            //using var context = _contextFactory.CreateDbContext();
            var trip = _mapper.Map<Trip>(input);
            await _context.Trips.AddAsync(trip);
            await _context.SaveChangesAsync();
            _backgroundJobClient.Enqueue<TripService>(svc => svc.PostCreateTripPublishNotificationsAsync(trip));
            _backgroundJobClient.Enqueue<TripService>(svc => svc.PostCreateTripUpdateFeatureAsync(trip));
            return trip;
        }

        public async Task PostCreateTripPublishNotificationsAsync(Trip trip)
        {
            var members = await _context.OrganizerMembers.Where(m => m.OrganizerId == trip.OrganizerId).ToListAsync();
            foreach (var member in members)
            {
                var notificationData = JsonSerializer.Serialize(new
                {
                    TripId = trip.Id,
                });
                _backgroundJobClient.Enqueue<NotificationService>(svc => svc.AddNotificationAsync(new Notification
                {
                    UserId = member.UserId,
                    Code = "trip_created",
                    Data = notificationData!
                }));
            }
        }

        public async Task PostCreateTripUpdateFeatureAsync(Trip trip)
        {
            var text = $"NAME: {trip.Name}\nDESCRIPTION: {trip.Description}";
            var embeddingFloats = await _embeddingService.GenerateEmbeddingAsync(text);
            var feature = await _context.TripFeatures.FindAsync(trip.Id);
            if (feature == null)
            {
                feature = new TripFeature
                {
                    TripId = trip.Id,
                    Embedding = new Pgvector.Vector(embeddingFloats)
                };
                await _context.TripFeatures.AddAsync(feature);
            }
            else
            {
                feature.Embedding = new Pgvector.Vector(embeddingFloats);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Trip> UpdateTripAsync(UpdateTripInput input)
        {
            //using var context = _contextFactory.CreateDbContext();

            var trip = await _context.Trips.FindAsync(input.Id);
            if (trip == null)
                throw new Exception("Trip not found");
            trip = _mapper.Map(input, trip);
            // itineraries
            foreach (var updateItineraryEntry in input.Itineraries)
            {
                if (updateItineraryEntry.Action == UpdateTripEntryAction.Create)
                {
                    var itineraryToCreate = _mapper.Map<TripItinerary>(updateItineraryEntry);
                    itineraryToCreate.TripId = trip.Id;
                    await _context.TripItineraries.AddAsync(itineraryToCreate);
                }
                else if (updateItineraryEntry.Action == UpdateTripEntryAction.Update)
                {
                    var itineraryToUpdate = await _context.TripItineraries.FindAsync(updateItineraryEntry.Id);
                    itineraryToUpdate = _mapper.Map(updateItineraryEntry, itineraryToUpdate);
                }
                else if (updateItineraryEntry.Action == UpdateTripEntryAction.Delete)
                {
                    var itineraryToDelete = await _context.TripItineraries.FindAsync(updateItineraryEntry.Id);
                    _context.TripItineraries.Remove(itineraryToDelete);
                }
            }
            // packages
            foreach (var updatePackageEntry in input.Packages)
            {
                if (updatePackageEntry.Action == UpdateTripEntryAction.Create)
                {
                    var packageToCreate = _mapper.Map<TripPackage>(updatePackageEntry);
                    packageToCreate.TripId = trip.Id;
                    await _context.TripPackages.AddAsync(packageToCreate);
                }
                else if (updatePackageEntry.Action == UpdateTripEntryAction.Update)
                {
                    var packageToUpdate = await _context.TripPackages.FindAsync(updatePackageEntry.Id);
                    packageToUpdate = _mapper.Map(updatePackageEntry, packageToUpdate);
                }
                else if (updatePackageEntry.Action == UpdateTripEntryAction.Delete)
                {
                    var packageToDelete = await _context.TripPackages.FindAsync(updatePackageEntry.Id);
                    _context.TripPackages.Remove(packageToDelete);
                }
            }
            // addons
            foreach (var updateAddonEntry in input.Addons)
            {
                if (updateAddonEntry.Action == UpdateTripEntryAction.Create)
                {
                    var addonToCreate = _mapper.Map<TripAddon>(updateAddonEntry);
                    addonToCreate.TripId = trip.Id;
                    await _context.TripAddons.AddAsync(addonToCreate);
                }
                else if (updateAddonEntry.Action == UpdateTripEntryAction.Update)
                {
                    var addonToUpdate = await _context.TripAddons.FindAsync(updateAddonEntry.Id);
                    addonToUpdate = _mapper.Map(updateAddonEntry, addonToUpdate);
                }
                else if (updateAddonEntry.Action == UpdateTripEntryAction.Delete)
                {
                    var addonToDelete = await _context.TripAddons.FindAsync(updateAddonEntry.Id);
                    _context.TripAddons.Remove(addonToDelete);
                }
            }
            // schedules
            foreach (var updateScheduleEntry in input.Schedules)
            {
                if (updateScheduleEntry.Action == UpdateTripEntryAction.Create)
                {
                    var scheduleToCreate = _mapper.Map<TripSchedule>(updateScheduleEntry);
                    scheduleToCreate.TripId = trip.Id;
                    await _context.TripSchedules.AddAsync(scheduleToCreate);
                }
                else if (updateScheduleEntry.Action == UpdateTripEntryAction.Update)
                {
                    var scheduleToUpdate = await _context.TripSchedules.FindAsync(updateScheduleEntry.Id);
                    scheduleToUpdate = _mapper.Map(updateScheduleEntry, scheduleToUpdate);
                }
                else if (updateScheduleEntry.Action == UpdateTripEntryAction.Delete)
                {
                    var scheduleToDelete = await _context.TripSchedules.FindAsync(updateScheduleEntry.Id);
                    _context.TripSchedules.Remove(scheduleToDelete!);
                }
            }
            await _context.SaveChangesAsync();
            return trip;
        }

        public async Task<IQueryable<Trip>> GetRelevantTripsAsync(int tripId)
        {
            var feature = await _context.TripFeatures.FirstOrDefaultAsync(tf => tf.TripId == tripId);
            if (feature == null)
                throw new Exception("Not Found");
            return _context.Trips
                .Where(t => t.Id != tripId)
                .OrderBy(t => t.Feature.Embedding.CosineDistance(feature.Embedding))
                .AsQueryable();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
