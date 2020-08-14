using System;
using System.Collections.Generic;
using System.Text;
using TraceIt.Models;
using TraceIt.Models.Query_Models;
using Xamarin.Forms;

namespace TraceIt.Views.Data_Templates
{
    public class StandardSearchDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SubjectTemplate { get; set; }
        public DataTemplate SubfieldTemplate { get; set; }
        public DataTemplate StandardTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Subject)
                return SubjectTemplate;
            else if (item is SubfieldModel)
                return SubfieldTemplate;
            else if (item is AssessmentStandard)
                return StandardTemplate;
            else
                throw new Exception("Invalid item type used");
        }
    }
}
