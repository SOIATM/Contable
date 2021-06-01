Imports Telerik.WinControls
Public Class Listas
    Public tecla As Boolean
    Public sinavi As String
    Public nvo_edit As String
    Dim comprobar As Boolean
    Dim Cargando As Boolean
    Public Event Enters()
    Public Event Cambio_item(value As String, texto As String)
    Public Event Cambio_texto(value As String, texto As String)
    Public Event Valida_fact(ByVal value1 As String, ByVal texto1 As String)
    Public Event Buscar_vin()
    Public Property SelectText() As String
        Get
            Return Me.Combo.Text
        End Get
        Set(ByVal value As String)
            Me.Combo.Text = value
            For Each Item In Me.Combo.Items
                If Me.Combo.Items(Item.RowIndex).DisplayValue = value Then
                    Me.Combo.SelectedValue = Me.Combo.Items(Item.RowIndex).Value
                    Dim ee As New System.EventArgs()
                    Me.Combo_SelectedValueChanged(Me.Combo, ee)
                    Exit For
                End If
            Next
        End Set
    End Property
    Public Sub Limpiar()
        Try
            Combo.DataSource = Nothing
            Combo.DisplayMember = Nothing
            Combo.ValueMember = Nothing
        Catch ex As Exception
            RadMessageBox.Show("Error al cargar los datos:" & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Public Property SelectItem() As Object
        Get
            Try
                Return Eventos.Valor(Me.Combo.SelectedValue)
            Catch ex As Exception

            End Try

        End Get

        Set(ByVal value As Object)
            Try
                Me.Combo.SelectedValue = value
                Dim ee As New System.EventArgs()
                Me.Combo_SelectedValueChanged(Me.Combo, ee)
            Catch
            End Try
        End Set

    End Property

    Public Sub Cargar(ByVal sql As String)
        Cargando = True
        Dim ds As DataSet
        Try
            ds = Eventos.Obtener_DS(sql)
            Combo.DataSource = ds.Tables(0)
            Combo.DisplayMember = ds.Tables(0).Columns(1).ColumnName
            Combo.ValueMember = ds.Tables(0).Columns(0).ColumnName
        Catch ex As Exception
            RadMessageBox.Show("Error al cargar los datos:: " & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
        Cargando = False
    End Sub

    Private Sub Combo_Enter(sender As Object, e As System.EventArgs) Handles Combo.Enter
        comprobar = True
    End Sub

    Private Sub Combo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Combo.SelectedValueChanged
        If Cargando = False Then
            If Me.Combo.Text <> "" Then
                RaiseEvent Cambio_item(Me.Combo.SelectedValue.ToString, Me.Combo.Text)
            End If
        End If
    End Sub

    Private Sub Combo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Combo.SelectedIndexChanged
        If Cargando = False Then
            If Me.Combo.Text <> "" Then
                RaiseEvent Cambio_texto(Me.Combo.SelectedValue.ToString, Me.Combo.Text)
            End If
        End If


    End Sub

    Private Sub Combo_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Combo.KeyUp

        Dim i As Integer = 0
        Dim dsf As String = ""
        Dim itm As String = ""
        If e.KeyValue = (Keys.Enter) Then
            If comprobar = True Then
                RaiseEvent Enters()
            End If
            e.Handled = True
        End If
    End Sub


    Public Function Orden(ByVal modulo As Boolean)
        sinavi = modulo
        Return sinavi
    End Function

    Public Function Valida_accion(ByVal acc As String)
        nvo_edit = acc
        Return nvo_edit
    End Function


    Private Sub Combo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Combo.LostFocus
        If Me.Combo.Text <> "" Then
            RaiseEvent Buscar_vin()
        End If
    End Sub

    Private Sub Combo_SelectedIndexChanged(sender As Object, e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles Combo.SelectedIndexChanged

    End Sub
End Class
