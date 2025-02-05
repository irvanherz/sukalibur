using AppAny.HotChocolate.FluentValidation;

namespace Sukalibur.Graph.Auth
{
    [ExtendObjectType(typeof(Mutation))]
    public class AuthMutationResolvers
    {
        [UseMutationConvention(PayloadFieldName = "data")]
        public async Task<AuthResult> Signup(SignupInput input, [Service] AuthService authService)
        {
            var result = await authService.SignupAsync(input);
            return result;
        }

        [UseMutationConvention(PayloadFieldName = "data")]
        public async Task<AuthResult> Signin([UseFluentValidation] SigninInput input, [Service] AuthService authService)
        {
            var result = await authService.SigninAsync(input);
            return result;
        }

        [UseMutationConvention(PayloadFieldName = "data")]
        public async Task<AuthResult> RefreshToken(RefreshTokenInput? input, [Service] AuthService authService)
        {
            var result = await authService.RefreshTokenAsync(input);
            return result;
        }

        [UseMutationConvention(PayloadFieldName = "data")]
        public async Task<bool> Signout(SignoutInput? input, [Service] AuthService authService)
        {
            await authService.SignoutAsync(input);
            return true;
        }
    }
}
