Imports Telerik.WinControls
Public Class DialogUnaSeleccion
    Dim max As Integer, min As Integer = 1
    Public Property ruta_icono() As System.Drawing.Bitmap
        Get
            Return Me.icono.Image
        End Get
        Set(ByVal value As System.Drawing.Bitmap)
            Me.icono.Image = value
        End Set
    End Property
    Public Property MaxSeleccion() As Integer
        Get
            Return max
        End Get
        Set(ByVal value As Integer)
            max = value
        End Set
    End Property
    Public Property MinSeleccion() As Integer
        Get
            Return min
        End Get
        Set(ByVal value As Integer)
            min = value
        End Set
    End Property
    Public Property Titulo() As String
        Get
            Return Me.Text
        End Get
        Set(ByVal value As String)
            Me.Text = value
        End Set
    End Property
    Public Property Texto() As String
        Get
            Return Me.txtTexto.Text
        End Get
        Set(ByVal value As String)
            Me.txtTexto.Text = value
        End Set
    End Property
    Public Property elementos() As String(,)
        Get
            Dim seleccionados As String(,)
            ReDim seleccionados(2, Me.chkLst.CheckedItems.Count - 1)
            For i As Integer = 0 To Me.chkLst.CheckedItems.Count - 1
                seleccionados(0, i) = Me.chkLst.CheckedItems.Item(i).ToString()
                seleccionados(1, i) = "1"
            Next
            Return seleccionados
        End Get
        Set(ByVal value As String(,))
            For i As Integer = 0 To value.GetLength(1) - 2
                Try
                    Me.chkLst.Items.Add(Valor(value(0, i).ToString), IIf(value(1, i) = "1", True, False))
                Catch ex As Exception

                End Try

            Next
        End Set
    End Property
    Public Sub limpiar()
        Me.chkLst.Items.Clear()
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Me.chkLst.CheckedItems.Count <= max And Me.chkLst.CheckedItems.Count >= min Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            RadMessageBox.Show("Solo puedes seleccionar entre " & min & " y " & max & " items, verifica tu selección...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

            'MessageBox.Show("Solo puedes seleccionar entre " & min & " y " & max & " items, verifica tu selección...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DialogUnaSeleccion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.chkLst.Items.Count < 1 Then
            RadMessageBox.Show("No se pudieron cargar los datos...", Eventos.titulo_app)
        End If
    End Sub

    Private Sub txtTexto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTexto.Click

    End Sub
End Class
