namespace Sukalibur.Graph.Auth
{
    [ExtendObjectType(typeof(Mutation))]
    public class AuthMutationResolvers
    {
        [UseMutationConvention]
        public async Task<AuthResult> RegisterUser(RegisterUserInput input, [Service] AuthService authService)
        {
            var result = await authService.RegisterUserAsync(input);
            return result;
        }

        [UseMutationConvention]
        public async Task<AuthResult> LoginUser(LoginUserInput input, [Service] AuthService authService)
        {
            var result = await authService.LoginUserAsync(input);
            return result;
        }

        [UseMutationConvention]
        public async Task<AuthResult> RefreshToken(RefreshTokenInput input, [Service] AuthService authService)
        {
            var result = await authService.RefreshTokenAsync(input);
            return result;
        }
    }
}
