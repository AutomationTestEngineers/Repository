using Configuration;
using NUnit.Framework;
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
            for (int j = 0; j < 3; j++)
            {
                ScreenBusy();
                try { driver.FindElement(By.XPath(xpath)); } catch { break; }
                var size = driver.FindElements(By.XPath(xpath)).Count();
                for (int i = 1; i <= size; i++)
                {
                    var q = FindBy(By.XPath($"{xpath}[{i}]//h6")).Text;
                    var question = q.Split('.')[1].ToString().Replace("?", "").Replace(" ", "").Trim().ToUpper();
                    string exp_answer = Questions.Get<string>(question) == null ? "NONEOFTHEABOVE" : Questions.Get<string>(question);
                    var answers = FindElements(By.XPath($"{xpath}[{i}]/div/label/label")).Select(t => t.Text.Replace(" ", "").Trim().ToUpper()).ToList();
                    var index = answers.FindIndex(s => s.Contains(exp_answer));
                    try
                    {
                        FindBy(By.XPath($"({xpath}[{i}]/div/label/input)[{index + 1}]"), 2, true).ClickCustom(driver);
                    }
                    catch (Exception e)
                    {
                        index = answers.FindIndex(s => s.Contains("NONEOFTHEABOVE"));
                        FindBy(By.XPath($"({xpath}[{i}]/div/label/input)[{index + 1}]"), 2, true).ClickCustom(driver);
                    }                 
                }
                btn_verid_answers_dispatch.ClickCustom(driver);
            }
        }
    }
}
