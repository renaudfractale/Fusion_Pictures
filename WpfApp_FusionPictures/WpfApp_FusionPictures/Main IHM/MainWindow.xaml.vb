Class MainWindow
    Private listeProjects As New List(Of UserControl_Project)
    Private Sub Button_AddProject_Click(sender As Object, e As RoutedEventArgs)
        Dim project As New UserControl_Project
        project.TextBox_NameProject.Text = "Project " + (listeProjects.Count).ToString
        StackPanel_Projects.Children.Add(project)
        listeProjects.Add(project)
    End Sub

    Private Sub Button_GenerateAll_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Button_UndoClose_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Button_About_Click(sender As Object, e As RoutedEventArgs)
        Dim About As New Window_About
        About.ShowDialog()
    End Sub
End Class
