Public Class Class_SizePicture
    Property X As Integer = 0
    Property Y As Integer = 0

    Public Overrides Function ToString() As String
        Return X.ToString + "x" + Y.ToString
    End Function

    Public Function IsValide() As Boolean
        Return Not X = 0
    End Function

    Public Sub SetByString(txt As String)
        Dim listeTXT As String() = txt.Split("x"c)
        If listeTXT.Length = 2 Then
            Me.X = CInt(listeTXT(0))
            Me.Y = CInt(listeTXT(1))
        End If
    End Sub
End Class
