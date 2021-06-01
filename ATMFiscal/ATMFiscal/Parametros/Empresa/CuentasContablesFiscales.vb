Imports Telerik.WinControls
Public Class CuentasContablesFiscales
    Private Sub CuentasContablesFiscales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Clientes()
        Eventos.DiseñoTabla(TablaImportar)
    End Sub
    Private Sub Cargar_Clientes()
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt

    End Sub

    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub

    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        Me.TablaImportar.Rows.Add()
    End Sub

    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaImportar.Rows
                If Fila.Cells(Descripcion.Index).Selected = True Then
                    If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                            If Me.TablaImportar.Item(ID.Index, i).Value <> Nothing Then
                                If Eventos.Comando_sql("Delete from dbo.CuentasContablesFiscales where Id_CtaFiscalesCon=" & Me.TablaImportar.Item(ID.Index, i).Value) > 0 Then
                                    RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                    Eventos.Insertar_usuariol("CtasContFisD", "Delete from dbo.CuentasContablesFiscales where Id_CtaFiscalesCon=" & Me.TablaImportar.Item(ID.Index, i).Value & "")
                                Else
                                    RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                                End If
                            End If
                        Next
                    End If
                    Me.CmdBuscarFact.PerformClick()
                End If
            Next
        End If

    End Sub

    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If

        If Me.lstCliente.SelectText <> "" Then

            Dim consulta As String = " SELECT 	Id_CtaFiscalesCon,	Cuenta,	Fiscal,Tipo,	Id_Empresa FROM dbo.CuentasContablesFiscales where Id_Empresa= " & Me.lstCliente.SelectItem & " "
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
                    Me.TablaImportar.Item(ID.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_CtaFiscalesCon")) = True, "", ds.Tables(0).Rows(j)("Id_CtaFiscalesCon"))
                    Me.TablaImportar.Item(Descripcion.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta")) = True, "", ds.Tables(0).Rows(j)("Cuenta"))
                    Me.TablaImportar.Item(Fiscal.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Fiscal")) = True, False, ds.Tables(0).Rows(j)("Fiscal"))
                    Me.TablaImportar.Item(Tipo.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Tipo")) = True, "", ds.Tables(0).Rows(j)("Tipo"))
                    frm.Barra.Value = j
                Next
                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
            Else
                Me.TablaImportar.RowCount = 2
                'Utilidad
                Me.TablaImportar.Item(ID.Index, 0).Value = ""
                Me.TablaImportar.Item(Descripcion.Index, 0).Value = "3050-0001-0000-0000"
                Me.TablaImportar.Item(Fiscal.Index, 0).Value = True
                Me.TablaImportar.Item(Tipo.Index, 0).Value = "Utilidad"
                'Perdida
                Me.TablaImportar.Item(ID.Index, 1).Value = ""
                Me.TablaImportar.Item(Descripcion.Index, 1).Value = "3050-0002-0000-0000"
                Me.TablaImportar.Item(Fiscal.Index, 1).Value = True
                Me.TablaImportar.Item(Tipo.Index, 1).Value = "Perdida"
                Me.CmdGuardarF.PerformClick()
            End If
        Else
            RadMessageBox.Show("Selecciona un Cliente...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            If Me.TablaImportar.Item(ID.Index, i).Value <> Nothing Then
                Edita_Registro(Me.TablaImportar.Item(ID.Index, i).Value, Me.TablaImportar.Item(Descripcion.Index, i).Value, Eventos.Bool2(Me.TablaImportar.Item(Fiscal.Index, i).Value), Me.TablaImportar.Item(Tipo.Index, i).Value.trim())
            Else
                Inserta_Registro(Me.TablaImportar.Item(Descripcion.Index, i).Value, Eventos.Bool2(Me.TablaImportar.Item(Fiscal.Index, i).Value), Me.TablaImportar.Item(Tipo.Index, i).Value.trim())
            End If
        Next
        Me.CmdBuscarFact.PerformClick()
    End Sub
    Private Sub Edita_Registro(ByVal Id_CtaFiscalesCon As Integer, ByVal Descripcion As String, ByVal Fisc As Integer, ByVal Tip As String)
        Dim sql As String = "UPDATE dbo.CuentasContablesFiscales
                            SET  
	                        Cuenta = '" & Descripcion & "',
                            Tipo =  '" & Tip & "' ,
                             Fiscal =  " & Fisc & " ,
                              Id_Empresa =  " & Me.lstCliente.SelectItem & "                              
	                        where Id_CtaFiscalesCon = " & Id_CtaFiscalesCon & "  "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("CuentasContablesFiscalesE", sql)
        End If
    End Sub
    Private Sub Inserta_Registro(ByVal Descripcion As String, ByVal Fisc As Integer, ByVal Tip As String)
        Dim sql As String = "INSERT INTO dbo.CuentasContablesFiscales
	                            (
	                            Cuenta,
	                            Fiscal,
                                Tipo,
	                            Id_Empresa
	                            )
                            VALUES 
                            	(
                            	'" & Trim(Descripcion.Replace("-", "")) & "',
                                " & Fisc & ",
                                 '" & Tip & "',
                            	" & Me.lstCliente.SelectItem & "
                            	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("CuentasContablesFiscalesI", sql)
        End If
    End Sub
End Class
