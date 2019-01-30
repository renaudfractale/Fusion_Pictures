Public Class Window_Select_SizePicture
    Property SizeListes As New Dictionary(Of String, Class_SizePicture)
    Property CurrentSize As String = ""
    Property IsOK As Boolean = False
    Private Sub GenerateListe()
        ComboBox_choose.Items.Clear()
        For Each Key In Me.SizeListes.Keys
            ComboBox_choose.Items.Add(Key)
            ComboBox_choose.Text = Key
            CurrentSize = ComboBox_choose.Text
        Next

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        GenerateListe()
    End Sub

    Private Sub Button_OK_Click(sender As Object, e As RoutedEventArgs)
        Me.IsOK = True
        Me.Close()
    End Sub

    Private Sub Button_Cancel_Click(sender As Object, e As RoutedEventArgs)
        Me.IsOK = False
        Me.Close()
    End Sub

    Private Sub ComboBox_choose_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        CurrentSize = ComboBox_choose.Text
    End Sub
End Class
