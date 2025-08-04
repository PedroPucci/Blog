using Blog.Application.ExtensionError;
using Blog.Application.Services.Interfaces;
using Blog.Domain.Dto;
using Blog.Domain.Entity;
using Blog.Infrastracture.RepositoryUoW;
using Blog.Shared.Logging;
using Blog.Shared.Validator;
using FluentValidation;
using Serilog;

namespace Blog.Application.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public PublicationService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public async Task<Result<PublicationEntity>> Add(PublicationEntity publicationEntity)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var isValidPublication = await IsValidPublicationRequest(publicationEntity);

                if (!isValidPublication.Success)
                {
                    Log.Error(LogMessages.InvalidPublicationInputs());
                    return Result<PublicationEntity>.Error(isValidPublication.Message);
                }

                publicationEntity.Title = publicationEntity.Title;
                publicationEntity.Content = publicationEntity.Content;
                var result = await _repositoryUoW.PublicationRepository.Add(publicationEntity);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<PublicationEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.AddingPublicationError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Error to add a new Publication");
            }
            finally
            {
                Log.Error(LogMessages.AddingPublicationSuccess());
                transaction.Dispose();
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PublicationEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<PublicationEntity>> Update(PublicationDto publicationDto)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var publicationById = await _repositoryUoW.PublicationRepository.GetById(publicationDto.Id);
                if (publicationById is null)
                    throw new InvalidOperationException("Error updating Publication");                

                var isValidPublication = await IsValidPublicationRequest(publicationById);
                if (!isValidPublication.Success)
                {
                    Log.Error(LogMessages.InvalidPublicationInputs());
                    return Result<PublicationEntity>.Error(isValidPublication.Message);
                }

                publicationById.ModificationDate = DateTime.UtcNow;
                publicationById.Title = publicationDto.Title;
                publicationById.Content = publicationDto.Content;

                _repositoryUoW.PublicationRepository.Update(publicationById);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<PublicationEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.UpdatingErrorPublication(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Error updating Publication", ex);
            }
            finally
            {
                Log.Error(LogMessages.UpdatingSuccessPublication());
                transaction.Dispose();
            }
        }

        private async Task<Result<PublicationEntity>> IsValidPublicationRequest(PublicationEntity publicationEntity)
        {
            var requestValidator = await new PublicationRequestValidator().ValidateAsync(publicationEntity);
            if (!requestValidator.IsValid)
            {
                string errorMessage = string.Join(" ", requestValidator.Errors.Select(e => e.ErrorMessage));
                errorMessage = errorMessage.Replace(Environment.NewLine, "");
                return Result<PublicationEntity>.Error(errorMessage);
            }

            return Result<PublicationEntity>.Ok();
        }
    }
}