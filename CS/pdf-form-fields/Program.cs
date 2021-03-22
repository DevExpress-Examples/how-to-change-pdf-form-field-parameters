using DevExpress.Pdf;
using System.Diagnostics;

namespace pdf_form_fields
{
    class Program
    {
        static void Main(string[] args)
        {
            using (PdfDocumentProcessor pdfDocumentProcessor = new PdfDocumentProcessor())
            {
                pdfDocumentProcessor.LoadDocument("Documents//FormDemo.pdf");

                PdfDocumentFacade documentFacade = pdfDocumentProcessor.DocumentFacade;
                PdfAcroFormFacade acroForm = documentFacade.AcroForm;

                //Change all form fields' color settings:
                var fields = acroForm.GetFields();
                foreach (PdfFormFieldFacade field in fields)
                {
                    ChangeFormFieldColor(field);
                    field.RebuildAppearance();
                }

                //Obtain button form field parameters:
                PdfButtonFormFieldFacade pushButton = acroForm.GetButtonFormField("Submit");
                PdfButtonWidgetFacade buttonWidget = pushButton.Widgets[0];
                
                //Specify a button icon and set its options:
                buttonWidget.SetNormalIcon("Documents//submit_3802014.png");
                buttonWidget.IconOptions.FitToAnnotationBounds = true;
                buttonWidget.IconOptions.ScaleCondition = PdfIconScalingCircumstances.BiggerThanAnnotationRectangle;
                buttonWidget.TextPosition = PdfWidgetAnnotationTextPosition.NoCaption;

                //Obtain the text form field properties
                
                PdfTextFormFieldFacade visaField = acroForm.GetTextFormField("VisaNo");

                //Divide field text into equally spaced positions:
                visaField.InputType = PdfTextFieldInputType.Comb;
                visaField.Multiline = false;
                
                //Limit the number of inserted characters:
                visaField.MaxLength = 8;

                //Enable multiline text in the text field:
                PdfTextFormFieldFacade addressField = acroForm.GetTextFormField("Address");                
                addressField.Multiline = true;

                addressField.Scrollable = true;
                addressField.SpellCheck = false;

                //Set the radio group value:
                PdfRadioGroupFormFieldFacade genderField = acroForm.GetRadioGroupFormField("Gender");
                genderField.Value = genderField.Field.Items[2].Value;
                
                //Change marker style for all radio buttons:
                foreach (PdfRadioButtonWidgetFacade widget in genderField.Widgets)
                {
                    widget.ButtonStyle = PdfAcroFormButtonStyle.Square;
                }

                //Set combo box field value:
                PdfComboBoxFormFieldFacade nationalityField = acroForm.GetComboBoxFormField("Nationality");
                nationalityField.Value = nationalityField.Items[68].Value;

                //Disable user input:
                nationalityField.Editable = false;
                
                //Disable multiple selection:
                nationalityField.MultiSelect = false;

                //Sort list items alphabetically:
                nationalityField.Sorted = true;                


                pdfDocumentProcessor.SaveDocument("FormDemo_new.pdf");
                Process.Start(new ProcessStartInfo("FormDemo_new.pdf") { UseShellExecute = true });
            }
        }

        private static void ChangeFormFieldColor(PdfFormFieldFacade field)
        {
            foreach (PdfWidgetFacade pdfWidget in field)
            {
                //Change color and border settings
                //For all form fields:
                pdfWidget.BorderWidth = 1;
                pdfWidget.BackgroundColor = new PdfRGBColor(0.81, 0.81, 0.81);
                pdfWidget.BorderColor = new PdfRGBColor(0.47, 0.44, 0.67);
                pdfWidget.FontColor = new PdfRGBColor(0.34, 0.25, 0.36);
                
                //Change border style for text form fields:
                if (field.Type == PdfFormFieldType.Text)
                {
                    pdfWidget.BorderStyle = PdfBorderStyle.Underline;
                }
            }
        }
    }
}

