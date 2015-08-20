using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using WMoney.Persistence.EntityFramework;
using WMoney.Persistence.Repositories;

namespace WMoney.Core.Bdd.User
{
    [Binding]
    public sealed class UserSteps
    {
        private string EMAIL_KEY = "email";
        private string PASSWORD_KEY = "password";
        private string DATA_CONTEXT_KEY = "dataContext";
        private string USER_CORE_KEY = "userCore";
        private string USER_REPOSITORY_KEY = "userRepository";
        private string RESULT_USER_KEY = "resultUser";


        #region Given
        
        [Given(@"the data context is fake")]
        public void GivenTheDataContextIsFake()
        {
            var dataContext = Substitute.For<WMoneyContext>();
            ScenarioContext.Current.Add(DATA_CONTEXT_KEY, dataContext);
        }

        [Given(@"the user repository is fake containing:")]
        public void GivenTheUserRepositoryIsFakeContaining(Table table)
        {
            var userRepository = Substitute.For<IUserRepository>();
            ScenarioContext.Current.Add(USER_REPOSITORY_KEY, userRepository);

            ScenarioContext.Current.Get<WMoneyContext>(DATA_CONTEXT_KEY)
                .UserRepository = userRepository;
        }

        [Given(@"the user core is ready")]
        public void GivenTheUserCoreIsReady()
        {
            var dataContext = ScenarioContext.Current.Get<WMoneyContext>(DATA_CONTEXT_KEY);
            
            var userCore = new UserCore(dataContext);
            ScenarioContext.Current.Add(USER_CORE_KEY, userCore);
        }

        [Given(@"the user has the email ""(.*)""")]
        public void GivenTheUserHasTheEmail(string email)
        {
            ScenarioContext.Current.Add(EMAIL_KEY, email);
        }

        [Given(@"the user has the password (.*)")]
        public void GivenTheUserHasThePassword(string password)
        {
            ScenarioContext.Current.Add(PASSWORD_KEY, password);
        } 
        #endregion

        #region When
        [When(@"the user core receives an user cration request")]
        public void WhenTheUserCoreReceivesAnUserCrationRequest()
        {
            var email = ScenarioContext.Current.Get<string>(EMAIL_KEY);
            var password = ScenarioContext.Current.Get<string>(PASSWORD_KEY);
            var userCore = ScenarioContext.Current.Get<IUserCore>(USER_CORE_KEY);

            var result = userCore.CreateUserAsync(email, password).Result;
            ScenarioContext.Current.Add(RESULT_USER_KEY, result);
        }
        #endregion

        #region Then
        
        #endregion

    }
}
