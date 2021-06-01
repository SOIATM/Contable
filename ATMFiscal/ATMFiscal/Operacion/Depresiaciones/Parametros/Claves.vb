Imports Telerik.WinControls
Public Class Claves
    Dim Activo As Boolean
    Private Sub Claves_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Activo = True
        Eventos.DiseñoTabla(Me.TablaImportar)
        Me.CmdBuscarFact.PerformClick()
        Activo = False
    End Sub

    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If

        Dim consulta As String = "SELECT Clave_Activos.Id_Clave,   Clave_Activos.Descripcion  FROM     Clave_Activos     "
        Dim ds As DataSet = Obtener_DS(consulta)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count

            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = ds.Tables(0).Rows.Count - 1
            Me.Cursor = Cursors.AppStarting
            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(j)
                Me.TablaImportar.Item(ID.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Clave")) = True, "", ds.Tables(0).Rows(j)("Id_Clave"))
                Me.TablaImportar.Item(Alia.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Descripcion")) = True, "", ds.Tables(0).Rows(j)("Descripcion"))
                frm.Barra.Value = j
            Next
            frm.Close()
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow

        End If

    End Sub

    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        Me.TablaImportar.Rows.Add()
    End Sub

    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            If Me.TablaImportar.Item(ID.Index, i).Value <> Nothing Then
                Edita_Registro(Me.TablaImportar.Item(ID.Index, i).Value, Me.TablaImportar.Item(Alia.Index, i).Value)
            Else
                Inserta_Registro(Me.TablaImportar.Item(Alia.Index, i).Value)
            End If
        Next
        Me.CmdBuscarFact.PerformClick()
    End Sub
    Private Sub Edita_Registro(ByVal Id_Persona_Cliente As Integer, ByVal Ali As String)
        Dim sql As String = "UPDATE dbo.Clave_Activos
                                SET  
    	                        Descripcion = '" & Ali & "' 
    	                        where Id_Clave = " & Id_Persona_Cliente & "  "
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Sub Inserta_Registro(ByVal Ali As String)
        Dim sql As String = "INSERT INTO dbo.Clave_Activos
    	                        (
    	                        Descripcion 
    	                        )
                                VALUES 
                                	(

                                	'" & Trim(Ali) & "' 
                                	)"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaImportar.Rows
                If Fila.Cells(Alia.Index).Selected = True Then
                    If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If Me.TablaImportar.Item(ID.Index, Fila.Index).Value <> Nothing Then
                            If Eventos.Comando_sql("Delete from dbo.Clave_Activos where Id_Clave=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value) > 0 Then
                                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                Eventos.Insertar_usuariol("Clave_ActivosD", "Delete from dbo.Clave_Activos where Id_Clave=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value & "")
                            Else
                                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                            End If
                        End If
                    End If
                    Me.CmdBuscarFact.PerformClick()
                End If
            Next
        End If

    End Sub

    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub
End Class
