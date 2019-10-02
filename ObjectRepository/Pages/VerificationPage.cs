using Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRepository.Pages
{
    public class VerificationPage : BasePage
    {
        public VerificationPage(IWebDriver driver) : base(driver) { }


        [FindsBy]
        private IWebElement btn_verid_answers_dispatch = null;


        string xpath = "//div[@class='row form-group radio-holder']";
       
        public void GiveAnswersForQuestions()
        {
            Sleep(300);
            var size = driver.FindElements(By.XPath(xpath)).Count();
            for (int i=1;i<= size; i++)
            {
                var question = FindBy(By.XPath($"{xpath}[{i}]//h6")).Text
                    .Split('.')[1].ToString().Replace("?", "").Trim();
                string act_answer = Questions.Get<string>(question);
                var answers = driver.FindElements(By.XPath($"{xpath}[{i}]/div/label/label"));
                for (int j = 1; j < answers.Count(); j++)
                {
                    var answer = answers[j - 1].Text;
                    if (answer.Contains(act_answer))
                    {
                        FindBy(By.XPath($"({xpath}[{i}]/div/label/input)[{j}]")).ClickCustom(driver);
                        break;
                    }
                }
            }
            btn_verid_answers_dispatch.ClickCustom(driver);
        }


        private void sample()
        {
            //if (question.Contains(""))
            //{
            //    var answers = driver.FindElements(By.XPath($"//div[@class='row form-group radio-holder'][{i}]/div/label/label"));
            //    for (int j = 1; j < answers.Count(); j++)
            //    {
            //        var answer = answers[j - 1].Text;
            //        if (answer.Contains(act_answer))
            //        {
            //            FindBy(By.XPath($"(//div[@class='row form-group radio-holder'][{i}]/div/label/input)[{j}]")).ClickCustom(driver);
            //            break;
            //        }
            //    }
            //}
        }
    }
}
