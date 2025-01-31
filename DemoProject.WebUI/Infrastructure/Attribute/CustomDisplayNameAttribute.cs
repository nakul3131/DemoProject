using DemoProject.Services.Abstract.MachineLearning;
using System.ComponentModel;

namespace DemoProject.WebUI.Infrastructure.Attribute
{
    public class CustomDisplayNameAttribute : DisplayNameAttribute
    {
        private IMLDetailRepository mlDetailRepository;

        CustomDisplayNameAttribute(IMLDetailRepository _mlDetailRepository)
        {
            mlDetailRepository = _mlDetailRepository;
        }

        // We pass display name from the constructor of the derived class (CustomDisplayNameAttribute) to the constructor 
        // of the base class i.e. DisplayNameAttribute(string displayName),  it is necessary to call base constructor.
        // In the inheritance hierarchy, always the base class constructor is called first.
        // In c#, the base keyword is used to access the base class constructor as shown below. 
        // Parameters:
        //  _displayName:
        //     The display name.        
        //  _languagePrmKey:
        //     The PrmKey of language. 
        public CustomDisplayNameAttribute(byte _input)
        {

        }
        public CustomDisplayNameAttribute(string _controllerName, string _viewName, string _displayName) : base(GetNameInGivenLanguage(_controllerName, _viewName, _displayName))
        {
        }

        // Get Name in to Given Language From Database Resource.
        private static string GetNameInGivenLanguage(string _controllerName, string _viewName, string _displayName)
        {
            // Access Non Static Member in Static Method Create Class Object
            CustomDisplayNameAttribute customDisplayNameAttribute = new CustomDisplayNameAttribute(1);
            return customDisplayNameAttribute.TranslateInRegionalLanguage(_controllerName, _viewName, _displayName);
        }

        // Access Non Static Member in Static Method Create Class Object
        private string TranslateInRegionalLanguage (string _controllerName, string _viewName, string _displayName)
        {
            return mlDetailRepository.TranslateInRegionalLanguage(_controllerName, _viewName, _displayName);
        }
    }
}