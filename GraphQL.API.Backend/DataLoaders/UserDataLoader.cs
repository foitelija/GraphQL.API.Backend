using FirebaseAdmin;
using FirebaseAdmin.Auth;
using GraphQL.API.Backend.Models;

namespace GraphQL.API.Backend.DataLoaders
{
    public class UserDataLoader : BatchDataLoader<string, UserType>
    {
        private readonly FirebaseAuth _firebaseAuth;
        public UserDataLoader(FirebaseApp firebaseApp, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
        }

        protected override async Task<IReadOnlyDictionary<string, UserType>> LoadBatchAsync(IReadOnlyList<string> userIds, CancellationToken cancellationToken)
        {
            var userIdentifiers = userIds.Select(x => new UidIdentifier(x)).ToList();
            var usersResult = await _firebaseAuth.GetUsersAsync(userIdentifiers);

            return usersResult.Users.Select(u => new UserType()
            {
                Id = u.Uid,
                Username = u.DisplayName,
                PhotoUrl = u.PhotoUrl
            }).ToDictionary(u=>u.Id);
        }
    }
}
