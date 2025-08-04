using Blog.Application.Services;
using Blog.Infrastracture.RepositoryUoW;

namespace Blog.Application.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IRepositoryUoW _repositoryUoW;
        //private readonly TokenService _tokenService;
        //private readonly BCryptoAlgorithm _crypto;

        private UserService userService;
        private PublicationService publicationService;

        //public UnitOfWorkService(IRepositoryUoW repositoryUoW, TokenService tokenService, BCryptoAlgorithm crypto)
        //{
        //    _repositoryUoW = repositoryUoW;
        //    _tokenService = tokenService;
        //    _crypto = crypto;
        //}

        public UnitOfWorkService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public UserService UserService
        {
            get
            {
                if (userService is null)
                    userService = new UserService(_repositoryUoW);
                return userService;
            }
        }

        public PublicationService PublicationService
        {
            get
            {
                if (publicationService is null)
                    publicationService = new PublicationService(_repositoryUoW);
                return publicationService;
            }
        }
    }
}