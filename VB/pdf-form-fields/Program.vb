Imports DevExpress.Pdf
Imports System.Diagnostics

Namespace pdf_form_fields

    Friend Class Program

        Shared Sub Main(ByVal args As String())
            Using pdfDocumentProcessor As PdfDocumentProcessor = New PdfDocumentProcessor()
                pdfDocumentProcessor.LoadDocument("Documents//FormDemo.pdf")
                Dim documentFacade As PdfDocumentFacade = pdfDocumentProcessor.DocumentFacade
                Dim acroForm As PdfAcroFormFacade = documentFacade.AcroForm
                'Change all form fields' color settings:
                Dim fields = acroForm.GetFields()
                For Each field As PdfFormFieldFacade In fields
                    Program.ChangeFormFieldColor(field)
                Next

                'Obtain button form field parameters:
                Dim pushButton As PdfButtonFormFieldFacade = acroForm.GetButtonFormField("Submit")
                Dim buttonWidget As PdfButtonWidgetFacade = pushButton.Widgets(0)
                'Specify a button icon and set its options:
                buttonWidget.SetNormalIcon("Documents//submit_3802014.png")
                buttonWidget.IconOptions.FitToAnnotationBounds = True
                buttonWidget.IconOptions.ScaleCondition = PdfIconScalingCircumstances.BiggerThanAnnotationRectangle
                buttonWidget.TextPosition = PdfWidgetAnnotationTextPosition.NoCaption
                'Obtain the text form field properties
                Dim visaField As PdfTextFormFieldFacade = acroForm.GetTextFormField("VisaNo")
                'Divide field text into equally spaced positions:
                visaField.InputType = PdfTextFieldInputType.Comb
                visaField.Multiline = False
                'Limit the number of inserted characters:
                visaField.MaxLength = 8
                'Enable multiline text in the text field:
                Dim addressField As PdfTextFormFieldFacade = acroForm.GetTextFormField("Address")
                addressField.Multiline = True
                addressField.Scrollable = True
                addressField.SpellCheck = False
                'Set the radio group value:
                Dim genderField As PdfRadioGroupFormFieldFacade = acroForm.GetRadioGroupFormField("Gender")
                genderField.Value = genderField.Field.Items(2).Value
                'Change marker style for all radio buttons:
                For Each widget As PdfRadioButtonWidgetFacade In genderField.Widgets
                    widget.ButtonStyle = PdfAcroFormButtonStyle.Square
                Next

                'Set combo box field value:
                Dim nationalityField As PdfComboBoxFormFieldFacade = acroForm.GetComboBoxFormField("Nationality")
                nationalityField.Value = nationalityField.Items(68).Value
                'Disable user input:
                nationalityField.Editable = False
                'Disable multiple selection:
                nationalityField.MultiSelect = False
                'Sort list items alphabetically:
                nationalityField.Sorted = True
                pdfDocumentProcessor.SaveDocument("FormDemo_new.pdf")
                Process.Start(New ProcessStartInfo("FormDemo_new.pdf") With {.UseShellExecute = True})
            End Using
        End Sub

        Private Shared Sub ChangeFormFieldColor(ByVal field As PdfFormFieldFacade)
            For Each pdfWidget As PdfWidgetFacade In field
                'Change color and border settings
                'For all form fields:
                pdfWidget.BorderWidth = 1
                pdfWidget.BackgroundColor = New PdfRGBColor(0.81, 0.81, 0.81)
                pdfWidget.BorderColor = New PdfRGBColor(0.47, 0.44, 0.67)
                pdfWidget.FontColor = New PdfRGBColor(0.34, 0.25, 0.36)
                'Change border style for text form fields:
                If field.Type Is PdfFormFieldType.Text Then
                    pdfWidget.BorderStyle = PdfBorderStyle.Underline
                End If
            Next
        End Sub
    End Class
End Namespace
