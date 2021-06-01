Imports Telerik.WinControls
Public Class Masivos
    Public Event Cmd_Guardar()
    Public Event Cerrar()
    Public Event Cmd_Editar(ByVal clave As String)

    Public Event Registro(ByVal clave As String)
    Public Property SqlSelect() As String
        Get
            Return Me.lblselect.Text
        End Get
        Set(ByVal value As String)
            Me.lblselect.Text = value
        End Set
    End Property
    Public Property CmdEditar_Enabled() As Boolean
        Get
            Return Me.CmdEditar.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.CmdEditar.Enabled = value
        End Set
    End Property
    Public Property CmdCerrar_enabled() As Boolean
        Get
            Return Me.CmdCerrar.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.CmdCerrar.Enabled = value
        End Set
    End Property
    Private Sub CmdEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdEditar.Click
        If Permiso(Me.Tag.ToString) Then
            If Tabla.RowCount > 0 Then
                RaiseEvent Cmd_Editar(Tabla.Item(0, Tabla.CurrentCell.RowIndex).Value)
                Refrescar(Me.lblselect.Text)
            Else
                RadMessageBox.Show("No hay registros para editar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
        Else
            RadMessageBox.Show("No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

        End If
    End Sub
    Public Sub Cargar()
        Refrescar(Me.lblselect.Text)
    End Sub
    Public Sub Refrescar(ByVal sql As String)

        Try
            If Me.Tabla.Rows.Count > 0 Then
                Me.Tabla.Rows.Clear()
            End If
            Dim DT As New DataTable

            Dim ds As DataSet = Eventos.Obtener_DS(sql)
            Dim p As Integer = 1
            If ds.Tables(0).Rows.Count > 0 Then
                Me.Tabla.RowCount = ds.Tables(0).Rows.Count
                DT = ds.Tables(0)
                If Me.Tabla.Columns.Count > 1 Then
                Else
                    For Each column As DataColumn In DT.Columns
                        Dim tipo As String = column.DataType.FullName
                        If tipo = "System.Boolean" Then
                            Dim ColumnaChek As New DataGridViewCheckBoxColumn

                            With ColumnaChek
                                .HeaderText = column.ColumnName
                                .Name = column.ColumnName
                                .Width = 80
                            End With

                            Me.Tabla.Columns.Insert(p, ColumnaChek)

                        Else
                            Me.Tabla.Columns.Add(column.ColumnName, column.ColumnName)
                        End If

                        p += 1
                    Next
                End If
                For j As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.Tabla.Item(j + 1, i).Value = ds.Tables(0).Rows(i)(j)
                    Next
                Next
            End If

        Catch ex As Exception
            RadMessageBox.Show("Error al cargar los datos de la tabla de detalle: ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
        Me.txtregistros.Text = Me.Tabla.RowCount
    End Sub
    Private Sub Tabla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tabla.Click
        If Tabla.RowCount > 0 Then
            RaiseEvent Registro(Tabla.Item(0, Tabla.CurrentCell.RowIndex).Value.ToString)
        End If
    End Sub
    Private Sub CmdCerrar_Click(sender As Object, e As EventArgs) Handles CmdCerrar.Click
        RaiseEvent Cerrar()
    End Sub

    Private Sub CmdNuevo_Click(sender As Object, e As EventArgs) Handles CmdNuevo.Click
        Me.Tabla.Rows.Add()
    End Sub
End Class
