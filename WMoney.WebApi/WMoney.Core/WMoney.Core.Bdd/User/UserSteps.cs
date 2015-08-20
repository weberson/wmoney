using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using WMoney.Persistence.EntityFramework;
using WMoney.Persistence.Repositories;
using WMoney.Persistence.Model;

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
        private string RESULT_EXCEPTION_KEY = "resultException";
        private string RESULT_KEY = "result";


        #region Given
        
        [Given(@"the data context is fake")]
        public void GivenTheDataContextIsFake()
        {
            var dataContext = Substitute.For<IWMoneyContext>();
            ScenarioContext.Current.Add(DATA_CONTEXT_KEY, dataContext);
        }

        [Given(@"the user repository is fake containing:")]
        public void GivenTheUserRepositoryIsFakeContaining(Table table)
        {
            var userRepository = Substitute.For<IUserRepository>();
            var users = new List<Persistence.Model.User>();

            foreach (var item in table.Rows)
            {
                var user = new Persistence.Model.User
                {
                    UserId = item.ContainsKey("UserId") ? Int32.Parse(item["UserId"]) : 0,
                    Email = item.ContainsKey("Email") ? item["Email"] : null,
                    Password = item.ContainsKey("Password") ? item["Password"] : null,
                };

                users.Add(user);
            }

            userRepository.AsQueryable().Returns(new TestDbAsyncEnumerable<Persistence.Model.User>(users.AsQueryable()));
            ScenarioContext.Current.Add(USER_REPOSITORY_KEY, userRepository);

            var context = ScenarioContext.Current.Get<IWMoneyContext>(DATA_CONTEXT_KEY);
            context
                .UserRepository
                .Returns(userRepository);
        }

        [Given(@"the user core is ready")]
        public void GivenTheUserCoreIsReady()
        {
            var dataContext = ScenarioContext.Current.Get<IWMoneyContext>(DATA_CONTEXT_KEY);
            
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

            try
            {
                var result = userCore.CreateUserAsync(email, password).Result;
                ScenarioContext.Current.Add(RESULT_USER_KEY, result);
            }
            catch (Exception ex)
            {
                ScenarioContext.Current.Add(RESULT_EXCEPTION_KEY, ex.InnerException.GetType().FullName);
            }
        }

        [When(@"the user core receives an user check request")]
        public void WhenTheUserCoreReceivesAnUserCheckRequest()
        {
            var email = ScenarioContext.Current.Get<string>(EMAIL_KEY);
            var password = ScenarioContext.Current.Get<string>(PASSWORD_KEY);
            var userCore = ScenarioContext.Current.Get<IUserCore>(USER_CORE_KEY);

            try
            {
                var result = userCore.CheckUserAsync(email, password).Result;
                ScenarioContext.Current.Add(RESULT_KEY, result);
            }
            catch (Exception ex)
            {
                ScenarioContext.Current.Add(RESULT_EXCEPTION_KEY, ex.InnerException.GetType().FullName);
            }
        }

        #endregion

        #region Then
        [Then(@"the result should be an user with email ""(.*)""")]
        public void ThenTheResultShouldBeAnUserWithEmail(string email)
        {
            var userResult = ScenarioContext.Current.Get<WMoney.Persistence.Model.User>(RESULT_USER_KEY);

            Assert.AreEqual(email, userResult.Email);
        }

        [Then(@"the result should be an user with password (.*)")]
        public void ThenTheResultShouldBeAnUserWithPassword(string password)
        {
            var userResult = ScenarioContext.Current.Get<WMoney.Persistence.Model.User>(RESULT_USER_KEY);

            Assert.AreEqual(password, userResult.Password);
        }

        [Then(@"the user should be saved on data context user repository")]
        public void ThenTheUserShouldBeSavedOnDataContextUserRepository()
        {
            var dataContext = ScenarioContext.Current.Get<IWMoneyContext>(DATA_CONTEXT_KEY);

            dataContext.UserRepository.Received(1).AddAsync(Arg.Any<Persistence.Model.User>(), true);
        }

        [Then(@"an exception of type ""(.*)"" should be thrown")]
        public void ThenAnExceptionOfTypeShouldBeThrown(string exception)
        {
            var resultException = ScenarioContext.Current.Get<string>(RESULT_EXCEPTION_KEY);

            Assert.AreEqual(exception, resultException);
        }

        [Then(@"the result should be ""(.*)""")]
        public void ThenTheResultShouldBe(bool resultExpected)
        {
            var actualResult = ScenarioContext.Current.Get<bool>(RESULT_KEY);

            Assert.AreEqual(resultExpected, actualResult);
        }

        #endregion

    }
}
