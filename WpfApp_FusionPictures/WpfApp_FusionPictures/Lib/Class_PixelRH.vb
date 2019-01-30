
Public Class Class_PixelRH
    Property R As Byte = 0
    Property G As Byte = 0
    Property B As Byte = 0
    Property Coef As Double = 0.0
    Public Sub New()

    End Sub
    Public Sub New(R As Byte, G As Byte, B As Byte, Optional Coef As Double = 0.0)
        Me.R = R
        Me.G = G
        Me.B = B
        Me.Coef = Coef
    End Sub
    Public Sub Add(NewP As Class_PixelRH)
        Dim NewR As Byte = NewP.R
        Dim NewG As Byte = NewP.G
        Dim NewB As Byte = NewP.B
        Dim NewCoef As Double = NewP.Coef
        Me.Add(NewR, NewG, NewB, NewCoef)
    End Sub



    Public Sub Add(NewR As Byte, NewG As Byte, NewB As Byte, NewCoef As Double)
        If (NewCoef + Me.Coef) > 0.0 Then
            Me.R = CByte((CDbl(Me.R) * Me.Coef + CDbl(NewR) * NewCoef) / (NewCoef + Me.Coef))
            Me.G = CByte((CDbl(Me.G) * Me.Coef + CDbl(NewG) * NewCoef) / (NewCoef + Me.Coef))
            Me.B = CByte((CDbl(Me.B) * Me.Coef + CDbl(NewB) * NewCoef) / (NewCoef + Me.Coef))
            Me.Coef += NewCoef
        End If

    End Sub
End Class