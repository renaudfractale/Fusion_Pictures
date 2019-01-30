Public Class UserControl_SpinBox
    Property Value As Double
    Property IsValide As Boolean
    Private Sub TextBox_root_TextChanged(sender As Object, e As TextChangedEventArgs)
        Value = 0.0
        IsValide = False
        Try
            Value = CDbl(TextBox_root.Text)
            IsValide = True
            TextBox_root.Background = Brushes.White
            TextBox_root.Foreground = Brushes.Black
        Catch ex As Exception
            TextBox_root.Background = Brushes.Red
            TextBox_root.Foreground = Brushes.White
        End Try
    End Sub
End Class
