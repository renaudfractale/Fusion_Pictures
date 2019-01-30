Public Module Module_Functions
    Public Function GetSizeOfImage(filename As String) As Class_SizePicture
        Dim SizePicture As New Class_SizePicture
        Try
            Dim img As System.Drawing.Image = System.Drawing.Image.FromFile(filename)
            SizePicture.X = img.Width
            SizePicture.Y = img.Height
            img.Dispose()  ' Removes file-lock of IIS
        Catch generatedExceptionName As OutOfMemoryException
            ' Image.FromFile throws an OutOfMemoryException  
            ' if the file does not have a valid image format or 
            ' GDI+ does not support the pixel format of the file. 
            ' 

        End Try

        Return SizePicture
    End Function
End Module
