Imports Telerik.WinControls
Public Class Clasificaciondecuentas
    Private Sub Clasificaciondecuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LstEmpresa.Cargar(" SELECT id_Empresa, Razon_social FROM Empresa ")
        Me.LstEmpresa.SelectItem = 1
        Eventos.DiseñoTabla(Me.TablaImportar)
    End Sub
    Private Sub Cargar()
        Me.LstEmpresa.Cargar(" SELECT id_Empresa, Razon_social FROM Empresa ")
        Me.LstEmpresa.SelectItem = 1
    End Sub

    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If

        If Me.LstEmpresa.SelectText <> "" Then

            Dim consulta As String = "Select 	Id_Clasificacion,	Descripcion,	Clave,	Balance,	Estado_de_Resultados 	 FROM dbo.ClasificacionCuentas where ID_empresa =" & Me.LstEmpresa.SelectItem & ""
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
                    Me.TablaImportar.Item(ID.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Clasificacion")) = True, "", ds.Tables(0).Rows(j)("Id_Clasificacion"))
                    Me.TablaImportar.Item(Desc.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Descripcion")) = True, "", ds.Tables(0).Rows(j)("Descripcion"))
                    Me.TablaImportar.Item(Cla.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Clave")) = True, "", ds.Tables(0).Rows(j)("Clave"))
                    Me.TablaImportar.Item(Bal.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Balance")) = True, 0, ds.Tables(0).Rows(j)("Balance"))
                    Me.TablaImportar.Item(Estad.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Estado_de_Resultados")) = True, 0, ds.Tables(0).Rows(j)("Estado_de_Resultados"))
                    frm.Barra.value = j
                Next
                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
            Else

                RadMessageBox.Show("No hay Registros de clasificacion para " & Me.LstEmpresa.SelectText & " ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
            End If
        Else
            RadMessageBox.Show("Selecciona una Empresa...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub
    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        Me.TablaImportar.Rows.Add()
    End Sub
    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1


            If Me.TablaImportar.Item(ID.Index, i).Value <> Nothing Then

                Edita_Registro(Me.TablaImportar.Item(ID.Index, i).Value, Me.TablaImportar.Item(Desc.Index, i).Value, Me.TablaImportar.Item(Cla.Index, i).Value, Eventos.Bool2(Me.TablaImportar.Item(Bal.Index, i).Value), Eventos.Bool2(Me.TablaImportar.Item(Estad.Index, i).Value))
            Else
                Inserta_Registro(Me.TablaImportar.Item(Desc.Index, i).Value, Me.TablaImportar.Item(Cla.Index, i).Value, Eventos.Bool2(Me.TablaImportar.Item(Bal.Index, i).Value), Eventos.Bool2(Me.TablaImportar.Item(Estad.Index, i).Value))
            End If
        Next
        Me.CmdBuscarFact.PerformClick()
    End Sub
    Private Sub Edita_Registro(ByVal Id_Clasificacion As Integer, ByVal Descripcion As String, ByVal Clave As String, ByVal Blan As Integer, ByVal Estad As Integer)

        Dim sql As String = " UPDATE dbo.ClasificacionCuentas SET 	Descripcion = '" & Descripcion & "',	Clave = '" & Clave & "',	Balance = " & Blan & ",	Estado_de_Resultados = " & Estad & "
					 where Id_Clasificacion = " & Id_Clasificacion & " and ID_empresa = " & Me.LstEmpresa.SelectItem & ""
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Sub Inserta_Registro(ByVal Descripcion As String, ByVal Clave As String, ByVal Blan As Integer, ByVal Estad As Integer)
        Dim sql As String = " INSERT INTO dbo.ClasificacionCuentas"
        sql &= " 	("
        sql &= " 	Descripcion,"
        sql &= " 	Clave,"
        sql &= " 	Balance,"
        sql &= " 	Estado_de_Resultados,"
        sql &= " 	ID_empresa"
        sql &= " 	)"
        sql &= " VALUES"
        sql &= " 	("
        sql &= " 	'" & Descripcion & "',"
        sql &= " 	'" & Clave & "',"
        sql &= " 	" & Blan & ","
        sql &= " 	" & Estad & ","
        sql &= " 	" & Me.LstEmpresa.SelectItem & ""
        sql &= " 	)"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaImportar.Rows
                If Fila.Cells(Desc.Index).Selected = True Then
                    If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then


                        If Me.TablaImportar.Item(ID.Index, Fila.Index).Value <> Nothing Then
                            If Eventos.Comando_sql("Delete from dbo.ClasificacionCuentas     where Id_Clasificacion=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value) > 0 Then
                                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                Eventos.Insertar_usuariol("Id_ClasificacionD", "Delete from dbo.ClasificacionCuentas where Id_Clasificacion=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value & "")
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

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Clasificacion")
    'End Sub
End Class