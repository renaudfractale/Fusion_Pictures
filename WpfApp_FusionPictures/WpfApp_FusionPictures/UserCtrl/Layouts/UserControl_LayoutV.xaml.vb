Imports System.Windows.Forms

Public Class UserControl_LayoutV
    Property PathFile As String = ""
    Property Size As New Class_SizePicture
    Private Sub Button_Close_Click(sender As Object, e As RoutedEventArgs)
        Me.Visibility = Visibility.Collapsed
    End Sub

    Private Sub Ctrl_Layout_Loaded(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Multiselect = False
        openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
        openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        If openFileDialog.ShowDialog = DialogResult.OK Then
            Size = Module_Functions.GetSizeOfImage(openFileDialog.FileName)
            If Size.isValide Then
                Image_apercu.Width = 200
                Dim myBitmapImage = New BitmapImage()
                myBitmapImage.BeginInit()
                myBitmapImage.UriSource = New Uri(openFileDialog.FileName)

                myBitmapImage.DecodePixelWidth = CInt(Image_apercu.Width)
                myBitmapImage.EndInit()

                Image_apercu.Source = myBitmapImage

                Me.PathFile = openFileDialog.FileName
                TextBlock_PictureName.Text = System.IO.Path.GetFileName(Me.PathFile) + " [" + Size.ToString + "]"
            Else
                Me.Visibility = Visibility.Collapsed
            End If
        Else
            Me.Visibility = Visibility.Collapsed
        End If
    End Sub
End Class
