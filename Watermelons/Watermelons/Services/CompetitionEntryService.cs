namespace Watermelons.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Watermelons.EntityFramework;
    using Watermelons.Repository;

    public class CompetitionEntryService : ICompetitionEntryService
    {
        private readonly ICompetitionEntryRepository _competitionEntryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompetitionEntryService(ICompetitionEntryRepository competitionEntryRepository, IUnitOfWork unitOfWork)
        {
            _competitionEntryRepository = competitionEntryRepository;
            _unitOfWork = unitOfWork;
        }

        public bool InsertCompetitionEntry(CompetitionEntry entry, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                if(entry.FavouriteWatermelonPlace != null)
                {
                    var wordCount = entry.FavouriteWatermelonPlace.Split(' ').Count();
                    if (wordCount > 100)
                    {
                        errorMessage = "Too many words entered";
                        return false;
                    }
                }
                _competitionEntryRepository.Insert(entry);
                _unitOfWork.SaveChanges();
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

            return true;
        }

        public bool EntryAlreadyExists(string email)
        {
            return _competitionEntryRepository.GetAll().Where(c => c.Email.Equals(email)).Count() > 0;
        }
    }
}
