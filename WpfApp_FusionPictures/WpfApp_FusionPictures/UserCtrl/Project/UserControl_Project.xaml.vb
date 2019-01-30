Imports System.Drawing
Imports System.Windows.Forms
Public Class UserControl_Project
    Private ListLayouts_CST As New List(Of UserControl_LayoutCST)
    Private ListLayouts_H As New List(Of UserControl_LayoutH)
    Private ListLayouts_V As New List(Of UserControl_LayoutV)
    Private ListLayouts_Circle As New List(Of UserControl_LayoutCircle)

    Private Function GetNumberLayout() As Integer
        Return ListLayouts_CST.Count + ListLayouts_H.Count + ListLayouts_V.Count + ListLayouts_Circle.Count
    End Function

    Private Sub Button_AddCST_Click(sender As Object, e As RoutedEventArgs)
        Dim LayoutCST As New UserControl_LayoutCST
        LayoutCST.TextBox_LayoutName.Text = "Layout " + GetNumberLayout().ToString
        StackPanel_layouts.Children.Add(LayoutCST)
        ListLayouts_CST.Add(LayoutCST)
    End Sub

    Private Sub Button_AddCircle_Click(sender As Object, e As RoutedEventArgs)
        Dim LayoutCircle As New UserControl_LayoutCircle
        LayoutCircle.TextBox_LayoutName.Text = "Layout " + GetNumberLayout().ToString
        StackPanel_layouts.Children.Add(LayoutCircle)
        ListLayouts_Circle.Add(LayoutCircle)
    End Sub

    Private Sub Button_AddV_Click(sender As Object, e As RoutedEventArgs)
        Dim LayoutV As New UserControl_LayoutV
        LayoutV.TextBox_LayoutName.Text = "Layout " + GetNumberLayout().ToString
        StackPanel_layouts.Children.Add(LayoutV)
        ListLayouts_V.Add(LayoutV)
    End Sub

    Private Sub Button_AddH_Click(sender As Object, e As RoutedEventArgs)
        Dim LayoutH As New UserControl_LayoutH
        LayoutH.TextBox_LayoutName.Text = "Layout " + GetNumberLayout().ToString
        StackPanel_layouts.Children.Add(LayoutH)
        ListLayouts_H.Add(LayoutH)
    End Sub

    Private Sub Button_Generate_Click(sender As Object, e As RoutedEventArgs)
        Cursor = System.Windows.Input.Cursors.Wait()
        Me.Button_Generate.IsEnabled = False
        Dim ListeSizeStrings As New Dictionary(Of String, Class_SizePicture)
        Dim CurrentSize As New Class_SizePicture
        For Each Item In ListLayouts_CST
            If Item.Visibility = Visibility.Visible Then
                If Not ListeSizeStrings.ContainsKey(Item.Size.ToString) Then
                    ListeSizeStrings.Add(Item.Size.ToString, Item.Size)
                    CurrentSize = Item.Size
                End If
            End If
        Next

        For Each Item In ListLayouts_H
            If Item.Visibility = Visibility.Visible Then
                If Not ListeSizeStrings.ContainsKey(Item.Size.ToString) Then
                    ListeSizeStrings.Add(Item.Size.ToString, Item.Size)
                    CurrentSize = Item.Size
                End If
            End If
        Next

        For Each Item In ListLayouts_V
            If Item.Visibility = Visibility.Visible Then
                If Not ListeSizeStrings.ContainsKey(Item.Size.ToString) Then
                    ListeSizeStrings.Add(Item.Size.ToString, Item.Size)
                    CurrentSize = Item.Size
                End If
            End If
        Next

        For Each Item In ListLayouts_Circle
            If Item.Visibility = Visibility.Visible Then
                If Not ListeSizeStrings.ContainsKey(Item.Size.ToString) Then
                    ListeSizeStrings.Add(Item.Size.ToString, Item.Size)
                    CurrentSize = Item.Size
                End If
            End If
        Next

        If ListeSizeStrings.Count > 0 Then
            If ListeSizeStrings.Count > 1 Then
                Dim WSelectSize As New Window_Select_SizePicture
                WSelectSize.SizeListes = ListeSizeStrings
                WSelectSize.ShowDialog()
                If WSelectSize.IsOK Then
                    CurrentSize.SetByString(WSelectSize.CurrentSize)
                Else
                    Cursor = System.Windows.Input.Cursors.Arrow()
                    Me.Button_Generate.IsEnabled = True
                    Exit Sub
                End If
            End If
            Dim Data(CurrentSize.X, CurrentSize.Y) As Class_PixelRH

            'Init Picture
            'Parallel.For(0, CurrentSize.X - 1, Sub(x)
            '                                       Parallel.For(0, CurrentSize.Y - 1, Sub(y)
            '                                                                              Data(x, y) = New Class_PixelRH
            '                                                                          End Sub)
            '                                   End Sub)

            For x As Integer = 0 To CurrentSize.X - 1
                For y As Integer = 0 To CurrentSize.Y - 1
                    Data(x, y) = New Class_PixelRH
                Next
            Next

            For Each Item In ListLayouts_CST
                If Item.Visibility = Visibility.Visible Then
                    Dim Coef As Double = Item.TextBox_ValueCST.Value
                    Dim PathFile As String = Item.PathFile
                    Dim PictureCurrent As Bitmap = ResizeImage(New Bitmap(PathFile), CurrentSize)
                    For x As Integer = 0 To CurrentSize.X - 1
                        For y As Integer = 0 To CurrentSize.Y - 1
                            Dim Pixel = PictureCurrent.GetPixel(x, y)
                            Data(x, y).Add(Pixel.R, Pixel.G, Pixel.B, Coef)
                        Next
                    Next
                End If
            Next

            For Each Item In ListLayouts_H
                If Item.Visibility = Visibility.Visible Then
                    Dim Coef_Start As Double = Item.TextBox_ValueStart.Value
                    Dim Coef_End As Double = Item.TextBox_ValueEnd.Value
                    Dim PathFile As String = Item.PathFile
                    Dim PictureCurrent As Bitmap = ResizeImage(New Bitmap(PathFile), CurrentSize)
                    For x As Integer = 0 To CurrentSize.X - 1
                        Dim Coef As Double = (x + 1) / CurrentSize.X * (Coef_End - Coef_Start) + Coef_Start
                        For y As Integer = 0 To CurrentSize.Y - 1
                            Dim Pixel = PictureCurrent.GetPixel(x, y)
                            Data(x, y).Add(Pixel.R, Pixel.G, Pixel.B, Coef)
                        Next
                    Next
                End If
            Next

            For Each Item In ListLayouts_V
                If Item.Visibility = Visibility.Visible Then
                    Dim Coef_Start As Double = Item.TextBox_ValueStart.Value
                    Dim Coef_End As Double = Item.TextBox_ValueEnd.Value
                    Dim PathFile As String = Item.PathFile
                    Dim PictureCurrent As Bitmap = ResizeImage(New Bitmap(PathFile), CurrentSize)
                    For x As Integer = 0 To CurrentSize.X - 1
                        For y As Integer = 0 To CurrentSize.Y - 1
                            Dim Coef As Double = (y + 1) / CurrentSize.Y * (Coef_End - Coef_Start) + Coef_Start
                            Dim Pixel = PictureCurrent.GetPixel(x, y)
                            Data(x, y).Add(Pixel.R, Pixel.G, Pixel.B, Coef)
                        Next
                    Next
                End If
            Next


            For Each Item In ListLayouts_Circle
                If Item.Visibility = Visibility.Visible Then
                    Dim Coef_Start As Double = Item.TextBox_ValueStart.Value
                    Dim Coef_End As Double = Item.TextBox_ValueEnd.Value
                    Dim PathFile As String = Item.PathFile
                    Dim PoseX As String = Item.ComboBox_PoseX.Text
                    Dim PoseY As String = Item.ComboBox_PoseY.Text

                    Dim Cx As Integer = 0
                    If PoseX = "Center" Then
                        Cx = CInt(CurrentSize.X / 2)
                    ElseIf PoseX = "Right" Then
                        Cx = CurrentSize.X - 1
                    End If

                    Dim Cy As Integer = 0
                    If PoseY = "Center" Then
                        Cy = CInt(CurrentSize.Y / 2)
                    ElseIf Posey = "Botton" Then
                        Cy = CurrentSize.Y - 1
                    End If

                    Dim Rmax = GetR(Cx, Cy, 0, 0)
                    Rmax = Math.Max(GetR(Cx, Cy, 0, CurrentSize.Y - 1), Rmax)
                    Rmax = Math.Max(GetR(Cx, Cy, CurrentSize.X, CurrentSize.Y - 1), Rmax)
                    Rmax = Math.Max(GetR(Cx, Cy, CurrentSize.X, 0 - 1), Rmax)


                    Dim PictureCurrent As Bitmap = ResizeImage(New Bitmap(PathFile), CurrentSize)
                    For x As Integer = 0 To CurrentSize.X - 1
                        For y As Integer = 0 To CurrentSize.Y - 1
                            Dim RCurrent As Double = GetR(Cx, Cy, x, y)
                            Dim Coef As Double = RCurrent / Rmax * (Coef_End - Coef_Start) + Coef_Start
                            Dim Pixel = PictureCurrent.GetPixel(x, y)
                            Data(x, y).Add(Pixel.R, Pixel.G, Pixel.B, Coef)
                        Next
                    Next
                End If
            Next


            Dim SaveFileDialog As New System.Windows.Forms.SaveFileDialog
            SaveFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            SaveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            If SaveFileDialog.ShowDialog = DialogResult.OK Then
                Dim FinalePicture = New Bitmap(CurrentSize.X, CurrentSize.Y)
                For x As Integer = 0 To CurrentSize.X - 1
                    For y As Integer = 0 To CurrentSize.Y - 1
                        FinalePicture.SetPixel(x, y, Color.FromArgb(255, Data(x, y).R, Data(x, y).G, Data(x, y).B))
                    Next
                Next

                FinalePicture.Save(SaveFileDialog.FileName)
                FinalePicture.Dispose()
            End If
            Cursor = System.Windows.Input.Cursors.Arrow()
            Me.Button_Generate.IsEnabled = True
        Else
            Cursor = System.Windows.Input.Cursors.Arrow()
            Me.Button_Generate.IsEnabled = True
            Exit Sub
        End If

    End Sub

    Private Function GetR(Cx As Integer, Cy As Integer, x As Integer, y As Integer) As Double
        Dim dx As Double = Math.Abs(Cx - x)
        Dim Dx2 = dx * dx

        Dim dy As Double = Math.Abs(Cy - y)
        Dim Dy2 = dy * dy

        Return Math.Sqrt(Dy2 + Dx2)
    End Function


    Private Function ResizeImage(ByVal InputImage As Bitmap, size As Class_SizePicture) As Bitmap
        Return New Bitmap(InputImage, New Size(size.X, size.Y))
    End Function

    Private Sub Button_Close_Click(sender As Object, e As RoutedEventArgs)
        Me.Visibility = Visibility.Collapsed
    End Sub
End Class
