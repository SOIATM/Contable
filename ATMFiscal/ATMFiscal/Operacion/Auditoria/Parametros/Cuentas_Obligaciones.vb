Imports Telerik.WinControls
Public Class Cuentas_Obligaciones

    Dim activo As Boolean
    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub

    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        Me.TablaImportar.Rows.Add()
        Buscar_Listas()
    End Sub
    Private Sub Diseño()
        Eventos.DiseñoTabla(Me.TablaImportar)
    End Sub
    Private Sub Buscar_Listas()
        Dim sql As String = "  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  
	                            FROM Catalogo_de_Cuentas WHERE Nivel1 >0 AND Nivel2 >=0 and Nivel3 >=0 and Id_Empresa = " & My.Forms.Inicio.Clt & " "
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

    Private Sub Cuentas_Obligaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Cargar()
        activo = False
    End Sub
    Private Sub Cargar()
        Me.LstEmpresa.Cargar(" SELECT id_Empresa, Razon_social FROM Empresa ")
        Me.LstEmpresa.SelectItem = 1
        Me.lstObligacion.Cargar(" SELECT Id_Obligacion, Descripcion FROM Obligaciones where id_Empresa= " & Me.LstEmpresa.SelectItem & " ")
        Me.lstObligacion.SelectText = ""
    End Sub

    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If

        If Me.LstEmpresa.SelectText <> "" Then

            Dim consulta As String = "SELECT Obligaciones_Cuentas.Id_Obligaciones_Ctas , Obligaciones_Cuentas.Alias "
            consulta &= " FROM     Obligaciones_Cuentas   where   Obligaciones_Cuentas.Id_Empresa = " & Me.LstEmpresa.SelectItem & " and Id_Obligacion = " & Me.lstObligacion.SelectItem & " "
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
                    Me.TablaImportar.Item(ID.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Obligaciones_Ctas")) = True, "", ds.Tables(0).Rows(j)("Id_Obligaciones_Ctas"))

                    Try
                        If Trim(ds.Tables(0).Rows(j)("Alias")) <> "" Then
                            Fila.Cells(Alia.Index).Value = Me.Alia.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Alias")), Me.Alia))
                        End If
                    Catch ex As Exception

                    End Try
                    frm.Barra.value = j
                Next
                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
            Else

                RadMessageBox.Show("No hay Cuentas para  " & Me.lstObligacion.SelectText & " de " & Me.LstEmpresa.SelectText & " ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
            End If
        Else
            RadMessageBox.Show("Selecciona una Empresa...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
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
    Private Function Buscar_cuenta(ByVal Cta As String)
        Dim Nombre As String = ""
        Nombre = "SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias FROM Catalogo_de_Cuentas WHERE   Cuenta =" & Cta & " and Id_Empresa= " & Me.LstEmpresa.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Nombre)
        If ds.Tables(0).Rows.Count > 0 Then
            Nombre = ds.Tables(0).Rows(0)(0)
        Else
            Nombre = ""
        End If
        Return Nombre
    End Function

    Private Sub Edita_Registro(ByVal Id_Obligaciones_Ctas As Integer, ByVal Ali As String, ByVal Cuenta As String)
        Dim sql As String = "UPDATE dbo.Obligaciones_Cuentas
	                            SET  Id_Obligacion =  " & Me.lstObligacion.SelectItem & " ,
		                        Alias = '" & Ali & "',
		                        Cuenta = '" & Cuenta & "'
		                        where Id_Obligaciones_Ctas = " & Id_Obligaciones_Ctas & "  "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Obligaciones_Cuentas E", sql)
        End If
    End Sub
    Private Sub Inserta_Registro(ByVal Ali As String, ByVal Cuenta As String)
        If Me.lstObligacion.SelectText <> "" Then


            Dim sql As String = "INSERT INTO dbo.Obligaciones_Cuentas
		                        (
	                            Id_Obligacion,
		                        Cuenta,
		                        Alias,
		                        Id_Empresa 

		                            )
	                            VALUES 
	                            	(
	                                " & Me.lstObligacion.SelectItem & ",
	                            	'" & Trim(Cuenta) & "',
	                            	'" & Trim(Ali) & "',
	                            	" & Me.LstEmpresa.SelectItem & "
	                            	)"
            If Eventos.Comando_sql(sql) > 0 Then
                Eventos.Insertar_usuariol("Obligaciones_CuentasI", sql)
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado una Obligación.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

        End If
    End Sub

    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            Dim Ctas As String() = Me.TablaImportar.Item(Alia.Index, i).Value.Split(New Char() {"/"c})
            Dim c As String = Trim(Ctas(0).ToString).Replace("-", "")
            If Me.TablaImportar.Item(ID.Index, i).Value <> Nothing Then
                Edita_Registro(Me.TablaImportar.Item(ID.Index, i).Value, Me.TablaImportar.Item(Alia.Index, i).Value, c)
            Else
                Inserta_Registro(Me.TablaImportar.Item(Alia.Index, i).Value, c)
            End If
        Next
        Me.CmdBuscarFact.PerformClick()
    End Sub

    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaImportar.Rows
                If Fila.Cells(Alia.Index).Selected = True Then
                    If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        If Me.TablaImportar.Item(ID.Index, Fila.Index).Value <> Nothing Then
                            If Eventos.Comando_sql("Delete from dbo.Obligaciones_Cuentas where Id_Obligaciones_Ctas=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value) > 0 Then
                                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                Eventos.Insertar_usuariol("Obligaciones_CuentasD", "Delete from dbo.Obligaciones_Cuentas where Id_Obligaciones_Ctas=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value & "")
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

End Class
