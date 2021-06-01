Imports Telerik.WinControls
Public Class Cuentas_IVA_AC
    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub
    Private Sub Cargar_Listas()
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
        Eventos.DiseñoTabla(Me.TablaImportar)
    End Sub
    Private Sub Buscar_Listas()
        Dim sql As String = "  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  
                            FROM Catalogo_de_Cuentas WHERE Nivel1 >= 1180 and Nivel1 <=1190 and Nivel4 = '0000' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        Dim RP As DataSet = Eventos.Obtener_DS(sql)
        If RP.Tables(0).Rows.Count > 0 Then


            If Me.Alia.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.Alia.Items.Add(RP.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If


    End Sub

    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If
        Dim consulta As String = " SELECT Id_ctas_IVA_AC, Cuenta,Abono FROM dbo.Cuentas_IVA_AC where Id_Empresa= " & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Obtener_DS(consulta)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
            Buscar_Listas()
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = ds.Tables(0).Rows.Count - 1
            Me.Cursor = Cursors.AppStarting
            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(j)
                Me.TablaImportar.Item(ID.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_ctas_IVA_AC")) = True, "", ds.Tables(0).Rows(j)("Id_ctas_IVA_AC"))

                Try
                    If Trim(ds.Tables(0).Rows(j)("Cuenta")) <> "" Then
                        Fila.Cells(Alia.Index).Value = Me.Alia.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Cuenta")), Me.Alia))
                    End If
                Catch ex As Exception

                End Try
                Me.TablaImportar.Item(Abono.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Abono")) = True, "", ds.Tables(0).Rows(j)("Abono"))

                frm.Barra.Value = j
            Next
            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            radMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay Cuentas  para Calculo de Ivas de la DIOT ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If

    End Sub
    Private Function Obtener_Index(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer = -1
        For i As Integer = 0 To Col.Items.Count - 1
            If valor = Trim(Col.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice
    End Function

    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        Me.TablaImportar.Rows.Add()
        Buscar_Listas()
    End Sub

    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
        If Me.TablaImportar.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaImportar.Rows
                If Fila.Cells(Alia.Index).Selected = True Then
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If Me.TablaImportar.Item(ID.Index, Fila.Index).Value <> Nothing Then
                            If Eventos.Comando_sql("Delete from dbo.Cuentas_IVA_AC where Id_ctas_IVA_AC=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value) > 0 Then
                                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                Eventos.Insertar_usuariol("Cuentas_DiotD", "Delete from dbo.Cuentas_IVA_AC where Id_ctas_IVA_AC=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value & "")
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

    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        If Eventos.Comando_sql("Delete from dbo.Cuentas_IVA_AC where Id_Empresa =" & Me.lstCliente.SelectItem & " ") > 0 Then
            Eventos.Insertar_usuariol("Cuentas_DiotE", "Delete from dbo.Cuentas_IVA_AC where Id_Empresa =" & Me.lstCliente.SelectItem & " ")
            For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                Dim Ctas As String() = Me.TablaImportar.Item(Alia.Index, i).Value.Split(New Char() {"/"c})
                Dim c As String = Trim(Ctas(0).ToString).Replace("-", "")
                Inserta_Registro(Me.TablaImportar.Item(Alia.Index, i).Value, i)
            Next
            Me.CmdBuscarFact.PerformClick()
        Else
            For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                Dim Ctas As String() = Me.TablaImportar.Item(Alia.Index, i).Value.Split(New Char() {"/"c})
                Dim c As String = Trim(Ctas(0).ToString).Replace("-", "")
                Inserta_Registro(Me.TablaImportar.Item(Alia.Index, i).Value, i)
            Next
            Me.CmdBuscarFact.PerformClick()
        End If
    End Sub
    Private Sub Inserta_Registro(ByVal Cuenta As String, ByVal I As Integer)
        Dim sql As String = "INSERT INTO dbo.Cuentas_IVA_AC 	( Cuenta ,Id_Empresa,Abono	) VALUES 	( '" & Cuenta & "' ," & Me.lstCliente.SelectItem & "," & Eventos.Bool2(IIf(IsDBNull(Me.TablaImportar.Item(2, I).Value) = True Or Me.TablaImportar.Item(2, I).Value.ToString() = "", 0, Me.TablaImportar.Item(2, I).Value)) & "	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Cargos_CeroI", sql)
        End If
    End Sub

    Private Sub CmdPlant_Click(sender As Object, e As EventArgs) Handles CmdPlant.Click
        Dim ds As DataSet = Eventos.Obtener_DS(" SELECT  Cuenta  FROM dbo.Cuentas_IVA_AC where Id_Empresa= " & Me.lstCliente.SelectItem & "")
        If ds.Tables(0).Rows.Count = 0 Then

            Dim sql As String = "INSERT INTO dbo.Cuentas_IVA_AC (  Cuenta, Id_Empresa,Abono)
                                VALUES (  '1190-0001-0007-0000 / IVA Retenido del mes actual', " & Me.lstCliente.SelectItem & ",0)
                                 

                                INSERT INTO dbo.Cuentas_IVA_AC (  Cuenta, Id_Empresa,Abono)
                                VALUES (  '1180-0001-0001-0000 / IVA Acreditable de Compras 16%', " & Me.lstCliente.SelectItem & ",0)
                                 

                                INSERT INTO dbo.Cuentas_IVA_AC (  Cuenta, Id_Empresa,Abono)
                                VALUES (  '1180-0001-0002-0000 / IVA Acreditable de Gastos 16%', " & Me.lstCliente.SelectItem & ",0)
                                

                                INSERT INTO dbo.Cuentas_IVA_AC (  Cuenta, Id_Empresa,Abono)
                                VALUES (  '1180-0001-0003-0000 / IVA Acred de Activos Fijos 16%', " & Me.lstCliente.SelectItem & ",0)
                                

                                INSERT INTO dbo.Cuentas_IVA_AC (  Cuenta, Id_Empresa,Abono)
                                VALUES (  '1180-0001-0004-0000 / IVA  Acred Activos Difer. 16%', " & Me.lstCliente.SelectItem & ",0)
                                 

                                INSERT INTO dbo.Cuentas_IVA_AC (  Cuenta, Id_Empresa,Abono)
                                VALUES (  '1180-0001-0005-0000 / IVA Acred. de Dev, Reb 16%', " & Me.lstCliente.SelectItem & ",1)
                           

                                INSERT INTO dbo.Cuentas_IVA_AC (  Cuenta, Id_Empresa,Abono)
                                VALUES (  '1180-0001-0006-0000 / IVA Acred. d Gastos Financ 16%', " & Me.lstCliente.SelectItem & ",0)
                               

                                INSERT INTO dbo.Cuentas_IVA_AC (  Cuenta, Id_Empresa,Abono)
                                VALUES  ('1180-0001-0007-0000 / IVA Retenido del mes actual', " & Me.lstCliente.SelectItem & ",0)
                             
                                "
            If Eventos.Comando_sql(sql) > 0 Then
                Eventos.Insertar_usuariol("PlanDiot", sql)
                Me.CmdBuscarFact.PerformClick()
            End If
        End If
    End Sub

    Private Sub Cuentas_IVA_AC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Listas()
        Me.CmdBuscarFact.PerformClick()

    End Sub

    Private Sub TablaImportar_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaImportar.DataError

    End Sub
End Class
