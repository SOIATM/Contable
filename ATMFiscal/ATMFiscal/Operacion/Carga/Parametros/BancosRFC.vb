Imports Telerik.WinControls
Public Class BancosRFC
    Public Event Registro(ByVal clave As String)
    Private Sub BancosRFC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Listas()
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

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.TablaImportar.Rows.Count > 0 Then
            Limpia()
            Me.lstCliente.SelectText = ""
            Me.LblFiltro.Text = ""
            Me.TxtFiltro.Text = ""
        End If
    End Sub
    Private Sub Limpia()
        Me.TablaImportar.Rows.Clear()
    End Sub
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        Limpia()
        If Me.lstCliente.SelectText <> "" Then
            Cargar_RFC()
        End If

    End Sub
    Private Sub Cargar_RFC()
        Dim posicion As Integer = 0
        Dim sql As String = " SELECT Bancos_RFC.Id_Ctas_Bancos, Bancos_RFC.RFC, Bancos_RFC.Nombre, Bancos_RFC.Clabe, 
	                                rtrim(Bancos.Clave) + '-' + Bancos.Nombre AS Banco, Bancos_RFC.Emitidas,Bancos_RFC.Favorito
	                                FROM     Bancos_RFC INNER JOIN
	                                Bancos ON Bancos_RFC.Id_Banco = Bancos.Id_Banco INNER JOIN
	                                Empresa ON Bancos_RFC.Id_Empresa = Empresa.Id_Empresa WHERE Emitidas = " & Eventos.Bool2(Me.RadEmitidas.Checked) & " and Bancos_RFC.Id_Empresa =" & Me.lstCliente.SelectItem & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count

            Cargar_bancosDev()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(i)
                Me.TablaImportar.Item(RF.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(1))
                Me.TablaImportar.Item(Nom.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(2))
                Me.TablaImportar.Item(Clave.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(3))
                Try
                    If Trim(ds.Tables(0).Rows(i)(4)) <> "" Then
                        Fila.Cells(Bank.Index).Value = Me.Bank.Items(Obtener_indexBDvBH(Trim(ds.Tables(0).Rows(i)(4))))
                    End If
                Catch ex As Exception

                End Try
                'Me.TablaImportar.Item(Bank.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(4))
                Me.TablaImportar.Item(Emit.Index, posicion).Value = IIf(Me.RadEmitidas.Checked = True, "SI", "NO")
                Me.TablaImportar.Item(ID.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(0))
                Me.TablaImportar.Item(Fav.Index, posicion).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)(6)) = True, False, ds.Tables(0).Rows(i)(6))
                posicion = posicion + 1

            Next
            If Me.RadEmitidas.Checked = True Then
                Dim Emitidas As String = " SELECT DISTINCT Xml_Sat.RFC_Receptor ,Xml_Sat.Nombre_Receptor  FROM Xml_Sat WHERE Xml_Sat.Id_Empresa =" & Me.lstCliente.SelectItem & " and  Emitidas = 1 and Xml_Sat.RFC_receptor not in (SELECT Bancos_RFC.RFC FROM     
	                                                Bancos_RFC INNER JOIN
	                                                Bancos ON Bancos_RFC.Id_Banco = Bancos.Id_Banco INNER JOIN
	                                                Empresa ON Bancos_RFC.Id_Empresa = Empresa.Id_Empresa WHERE Emitidas = 1  and Bancos_RFC.Id_Empresa =" & Me.lstCliente.SelectItem & ") "
                Dim em As DataSet = Eventos.Obtener_DS(Emitidas)
                If em.Tables(0).Rows.Count > 0 Then
                    Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + em.Tables(0).Rows.Count
                    For j As Integer = 0 To em.Tables(0).Rows.Count - 1

                        Me.TablaImportar.Item(RF.Index, posicion).Value = Trim(em.Tables(0).Rows(j)(0))
                        Me.TablaImportar.Item(Nom.Index, posicion).Value = Trim(em.Tables(0).Rows(j)(1))
                        Me.TablaImportar.Item(Emit.Index, posicion).Value = "SI"
                        Me.TablaImportar.Item(ID.Index, posicion).Value = ""

                        posicion = posicion + 1
                    Next
                End If
            Else
                Dim Recibidas As String = " SELECT DISTINCT Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor  FROM Xml_Sat WHERE Xml_Sat.Id_Empresa =" & Me.lstCliente.SelectItem & " and Emitidas = 0 and Xml_Sat.RFC_Emisor not in (SELECT Bancos_RFC.RFC FROM     
	                                                Bancos_RFC INNER JOIN
	                                                Bancos ON Bancos_RFC.Id_Banco = Bancos.Id_Banco INNER JOIN
	                                                Empresa ON Bancos_RFC.Id_Empresa = Empresa.Id_Empresa WHERE Emitidas = 0  and Bancos_RFC.Id_Empresa =" & Me.lstCliente.SelectItem & ") "
                Dim Re As DataSet = Eventos.Obtener_DS(Recibidas)
                If Re.Tables(0).Rows.Count > 0 Then
                    Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Re.Tables(0).Rows.Count
                    For j As Integer = 0 To Re.Tables(0).Rows.Count - 1

                        Me.TablaImportar.Item(RF.Index, posicion).Value = Trim(Re.Tables(0).Rows(j)(0))
                        Me.TablaImportar.Item(Nom.Index, posicion).Value = Trim(Re.Tables(0).Rows(j)(1))
                        Me.TablaImportar.Item(Emit.Index, posicion).Value = "NO"
                        Me.TablaImportar.Item(ID.Index, posicion).Value = ""
                        posicion = posicion + 1
                    Next
                End If
            End If


        Else

            If Me.RadEmitidas.Checked = True Then
                Dim Emitidas As String = " SELECT DISTINCT Xml_Sat.RFC_receptor ,Xml_Sat.Nombre_receptor  FROM Xml_Sat WHERE Emitidas = 1  and Id_Empresa =" & Me.lstCliente.SelectItem & " "
                Dim em As DataSet = Eventos.Obtener_DS(Emitidas)
                If em.Tables(0).Rows.Count > 0 Then
                    Me.TablaImportar.RowCount = em.Tables(0).Rows.Count
                    For j As Integer = 0 To em.Tables(0).Rows.Count - 1
                        Me.TablaImportar.Item(RF.Index, posicion).Value = Trim(em.Tables(0).Rows(j)(0))
                        Me.TablaImportar.Item(Nom.Index, posicion).Value = Trim(em.Tables(0).Rows(j)(1))
                        Me.TablaImportar.Item(Emit.Index, posicion).Value = "SI"
                        Me.TablaImportar.Item(ID.Index, posicion).Value = ""
                        posicion = posicion + 1
                    Next
                End If
            Else
                Dim Recibidas As String = " SELECT DISTINCT Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor  FROM Xml_Sat WHERE Emitidas = 0 and  Id_Empresa =" & Me.lstCliente.SelectItem & " "
                Dim Re As DataSet = Eventos.Obtener_DS(Recibidas)
                If Re.Tables(0).Rows.Count > 0 Then
                    Me.TablaImportar.RowCount = Re.Tables(0).Rows.Count
                    For j As Integer = 0 To Re.Tables(0).Rows.Count - 1
                        Me.TablaImportar.Item(RF.Index, posicion).Value = Trim(Re.Tables(0).Rows(j)(0))
                        Me.TablaImportar.Item(Nom.Index, posicion).Value = Trim(Re.Tables(0).Rows(j)(1))
                        Me.TablaImportar.Item(Emit.Index, posicion).Value = "NO"
                        Me.TablaImportar.Item(ID.Index, posicion).Value = ""
                        posicion = posicion + 1
                    Next
                End If
            End If

        End If

    End Sub
    Private Sub Verificar()
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            If Me.TablaImportar.Item(Nom.Index, i).Value <> Nothing And Me.TablaImportar.Item(RF.Index, i).Value <> Nothing And Me.TablaImportar.Item(Nom.Index, i).Value <> Nothing And Me.TablaImportar.Item(Clave.Index, i).Value <> Nothing And Me.TablaImportar.Item(Bank.Index, i).Value <> Nothing And Me.TablaImportar.Item(Emit.Index, i).Value <> Nothing Then
                Me.TablaImportar.Item(Verif.Index, i).Value = True
            Else
                Me.TablaImportar.Item(Verif.Index, i).Value = False
            End If
        Next




    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If Me.TablaImportar.Rows.Count > 0 Then
            For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                If Me.TablaImportar.Item(Verif.Index, i).Value = True Then
                    Dim banco As Integer = ObteneR_Banco(Me.TablaImportar.Item(Bank.Index, i).Value)
                    If Me.TablaImportar.Item(ID.Index, i).Value = "" Then
                        If Buscar_favorito(Me.TablaImportar.Item(RF.Index, i).Value, Me.TablaImportar.Item(Clave.Index, i).Value) = True Then
                            Inserta(IIf(Me.RadEmitidas.Checked = True, 1, 0), banco, Me.TablaImportar.Item(Clave.Index, i).Value, Me.TablaImportar.Item(RF.Index, i).Value, Me.TablaImportar.Item(Nom.Index, i).Value, Eventos.Bool2(Me.TablaImportar.Item(Fav.Index, i).Value))
                        End If
                    Else
                        If Buscar_favorito(Me.TablaImportar.Item(RF.Index, i).Value, Me.TablaImportar.Item(Clave.Index, i).Value) = True Then
                            Editar(Me.TablaImportar.Item(ID.Index, i).Value, IIf(Me.RadEmitidas.Checked = True, 1, 0), banco, Me.TablaImportar.Item(Clave.Index, i).Value, Me.TablaImportar.Item(RF.Index, i).Value, Me.TablaImportar.Item(Nom.Index, i).Value, Eventos.Bool2(Me.TablaImportar.Item(Fav.Index, i).Value))
                        End If
                    End If
                End If
            Next
        Else

        End If
        Me.CmdImportar.PerformClick()
    End Sub
    Private Function Buscar_favorito(ByVal rfc As String, ByVal Clave As String)
        Dim Hacer As Boolean
        Dim Sql As String = " Select * from Bancos_RFC where Emitidas =" & Eventos.Bool2(Me.RadEmitidas.Checked) & " and  rfc ='" & rfc & "' and Favorito= 1 and Id_Empresa  =" & Me.lstCliente.SelectItem & " "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Clave = ds.Tables(0).Rows(0)("clabe") Then
                Hacer = False
            Else
                Hacer = True
            End If
        Else
            Hacer = True
        End If
        Return Hacer
    End Function
    Private Sub TablaImportar_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellEndEdit

        If Trim(Me.TablaImportar.Item(Bank.Index, Me.TablaImportar.CurrentRow.Index).Value) <> "" Then
        Else
            Cargar_bancosDev()
        End If
        Verificar()
    End Sub
    Private Sub Editar(ByVal id As Integer, ByVal emitidas As Integer, ByVal banco As Integer, ByVal clabe As String, ByVal rfc As String, ByVal nombre As String, ByVal favorito As Integer)
        Dim sql As String = "UPDATE dbo.Bancos_RFC
	                              SET 
	                           	RFC = '" & rfc & "',
	                           	Nombre = '" & nombre & "',
	                           	Clabe = " & clabe & ",
	                           	Id_Banco = " & banco & ",
	                           	Emitidas = " & emitidas & ",
	                           	Id_Empresa =" & Me.lstCliente.SelectItem & " , Favorito = " & favorito & " where Id_Ctas_Bancos = " & id & " "
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Sub Inserta(ByVal emitidas As Integer, ByVal banco As Integer, ByVal clabe As String, ByVal rfc As String, ByVal nombre As String, ByVal Favorito As Integer)
        Dim sql As String = " INSERT INTO dbo.Bancos_RFC
	                                	(

	                                	RFC,
	                                	Nombre,
	                                	Clabe,
	                                	Id_Banco,
	                                	Emitidas,
	                                	Id_Empresa,Favorito
	                                	)
	                                VALUES 
	                                	(

	                                	'" & rfc & "',
	                                	'" & nombre & "',
	                                	'" & clabe & "',
	                                	" & banco & ",
	                                	" & emitidas & ",
	                                	" & Me.lstCliente.SelectItem & "," & Favorito & "
	                                	) "
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Sub Cargar_bancosDev()

        '***************************    Modificar codigo para los nuevos bancos por rfc    **********************************
        Dim sql2 As String = " SELECT rtrim(Bancos.Clave) +'-'+ Bancos.Nombre AS Alias FROM Bancos "
        Dim ds2 As DataSet = Eventos.Obtener_DS(sql2)
        If ds2.Tables(0).Rows.Count > 0 Then
            If Me.Bank.Items.Count = 0 Then
                For i As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                    Me.Bank.Items.Add(ds2.Tables(0).Rows(i)("Alias"))
                Next
            Else
                Me.Bank.Items.Clear()
                For i As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                    Me.Bank.Items.Add(ds2.Tables(0).Rows(i)("Alias"))
                Next
            End If
        End If


    End Sub
    Private Function Obtener_indexBDvBH(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.Bank.Items.Count - 1
            If valor = Trim(Me.Bank.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function ObteneR_Banco(ByVal clave As String)
        Dim banco As String
        Dim posi As Integer = InStr(1, clave, "-", CompareMethod.Binary)
        clave = clave.Substring(0, posi - 1)
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Id_Banco FROM dbo.Bancos WHERE  clave= '" & clave & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            banco = ds.Tables(0).Rows(0)("Id_Banco")
        Else banco = 0
        End If
        Return banco
    End Function

    Private Sub CmdDuplicar_Click(sender As Object, e As EventArgs) Handles CmdDuplicar.Click
        For Each Fila As DataGridViewRow In TablaImportar.Rows
            If Fila.Cells(RF.Index).Selected = True Then
                Me.TablaImportar.Rows.Add(False, Fila.Cells(RF.Index).Value, Fila.Cells(Nom.Index).Value, "", "", IIf(Me.RadEmitidas.Checked = True, "SI", "NO"), "")
            End If
        Next
    End Sub

    Private Sub TxtFiltro_TextChanged(sender As Object, e As EventArgs) Handles TxtFiltro.TextChanged
        If Me.lstCliente.SelectText <> "" Then
            If Me.LblFiltro.Text <> "" Then
                Me.TablaImportar.Rows.Clear()
                Cargar_bancosDev()
                Dim posicion As Integer = 0
                Dim sql As String = " SELECT Bancos_RFC.Id_Ctas_Bancos, Bancos_RFC.RFC, Bancos_RFC.Nombre, Bancos_RFC.Clabe, 
	                                rtrim(Bancos.Clave) + '-' + Bancos.Nombre AS Banco, Bancos_RFC.Emitidas,Bancos_RFC.Favorito
	                                FROM     Bancos_RFC INNER JOIN
	                                Bancos ON Bancos_RFC.Id_Banco = Bancos.Id_Banco INNER JOIN
	                                Empresa ON Bancos_RFC.Id_Empresa = Empresa.Id_Empresa WHERE Emitidas = " & Eventos.Bool2(Me.RadEmitidas.Checked) & " and Bancos_RFC.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_RFC." & Me.LblFiltro.Text & " like '%" & Me.TxtFiltro.Text & "%' "
                Dim ds As DataSet = Eventos.Obtener_DS(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count

                    Cargar_bancosDev()
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(i)
                        Me.TablaImportar.Item(RF.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(1))
                        Me.TablaImportar.Item(Nom.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(2))
                        Me.TablaImportar.Item(Clave.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(3))
                        Try
                            If Trim(ds.Tables(0).Rows(i)(4)) <> "" Then
                                Fila.Cells(Bank.Index).Value = Me.Bank.Items(Obtener_indexBDvBH(Trim(ds.Tables(0).Rows(i)(4))))
                            End If
                        Catch ex As Exception

                        End Try
                        'Me.TablaImportar.Item(Bank.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(4))
                        Me.TablaImportar.Item(Emit.Index, posicion).Value = IIf(Me.RadEmitidas.Checked = True, "SI", "NO")
                        Me.TablaImportar.Item(ID.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(0))
                        Me.TablaImportar.Item(Fav.Index, posicion).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)(6)) = True, False, ds.Tables(0).Rows(i)(6))
                        posicion = posicion + 1

                    Next

                Else
                    If Me.RadEmitidas.Checked = True Then
                        Dim Emitidas As String = " SELECT DISTINCT Xml_Sat.RFC_receptor ,Xml_Sat.Nombre_receptor  FROM Xml_Sat WHERE  Emitidas = 1  and  Id_Empresa =" & Me.lstCliente.SelectItem & " and " & Me.LblFiltro.Text & "_receptor like '%" & Me.TxtFiltro.Text & "%'  "
                        Dim em As DataSet = Eventos.Obtener_DS(Emitidas)
                        If em.Tables(0).Rows.Count > 0 Then
                            Me.TablaImportar.RowCount = em.Tables(0).Rows.Count
                            For j As Integer = 0 To em.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(RF.Index, posicion).Value = Trim(em.Tables(0).Rows(j)(0))
                                Me.TablaImportar.Item(Nom.Index, posicion).Value = Trim(em.Tables(0).Rows(j)(1))
                                Me.TablaImportar.Item(Emit.Index, posicion).Value = "SI"
                                Me.TablaImportar.Item(ID.Index, posicion).Value = ""
                                posicion = posicion + 1
                            Next
                        End If
                    Else
                        Dim Recibidas As String = " SELECT DISTINCT Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor  FROM Xml_Sat WHERE Emitidas = 0  and  Id_Empresa =" & Me.lstCliente.SelectItem & " and " & Me.LblFiltro.Text & "_Emisor like '%" & Me.TxtFiltro.Text & "%' "
                        Dim Re As DataSet = Eventos.Obtener_DS(Recibidas)
                        If Re.Tables(0).Rows.Count > 0 Then
                            Me.TablaImportar.RowCount = Re.Tables(0).Rows.Count
                            For j As Integer = 0 To Re.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(RF.Index, posicion).Value = Trim(Re.Tables(0).Rows(j)(0))
                                Me.TablaImportar.Item(Nom.Index, posicion).Value = Trim(Re.Tables(0).Rows(j)(1))
                                Me.TablaImportar.Item(Emit.Index, posicion).Value = "NO"
                                Me.TablaImportar.Item(ID.Index, posicion).Value = ""
                                posicion = posicion + 1
                            Next
                        End If
                    End If

                End If

            End If
        End If
    End Sub

    Private Sub TablaImportar_Click(sender As Object, e As EventArgs) Handles TablaImportar.Click
        If TablaImportar.RowCount > 0 Then
            RaiseEvent Registro(TablaImportar.Item(0, TablaImportar.CurrentCell.RowIndex).Value.ToString)
            Me.LblFiltro.Text = Me.TablaImportar.Columns(Me.TablaImportar.CurrentCell.ColumnIndex).HeaderText
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles CmdEliminar.Click
        For Each Fila As DataGridViewRow In TablaImportar.Rows
            If Fila.Cells(Verif.Index).Selected = True Then

                Elimina(Fila.Cells(RF.Index).Value, Eventos.Bool2(Me.RadEmitidas.Checked), Eventos.Bool2(Fila.Cells(Fav.Index).Value), Fila.Cells(ID.Index).Value)
            End If
        Next
        Me.CmdImportar.PerformClick()
    End Sub
    Private Sub Elimina(ByVal Rfc As String, ByVal Emitidas As Integer, ByVal Favorito As Integer, ByVal id As Integer)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("Realmente deseas eliminar el Banco del RFC: " & Rfc & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Eventos.Comando_sql("DELETE FROM dbo.Bancos_RFC WHERE Id_Ctas_Bancos =" & id) > 0 Then
                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

            Else
                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End If

    End Sub

    Private Sub TxtFiltro_MarginChanged(sender As Object, e As EventArgs) Handles TxtFiltro.MarginChanged

    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Asignación de Bancos para los RFC´s de emitidas y recibidas")
    'End Sub


End Class