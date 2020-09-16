using AutoMapper;
using Homework_Adform.CommonLibrary.Helpers;

namespace Homework_Adform.Tests
{
    /// <summary>
    /// Automapper initiator.
    /// </summary>
    public class MapperInitiator
    {
        protected MapperInitiator()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });

            Mapper = mappingConfig.CreateMapper();
        }

        public IMapper Mapper { get; }
    }
}
