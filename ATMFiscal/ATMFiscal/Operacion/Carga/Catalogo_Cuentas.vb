Imports Telerik.WinControls
Public Class Catalogo_Cuentas
	Dim activo As Boolean
	Dim Prin As Boolean
	Dim nuevo As Boolean
	Public Event Registro(ByVal clave As String)
	Private Sub Catalogo_Cuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		activo = True
        Cargar_listas()
        Eventos.DiseñoTabla(Me.TablaCat)
        Limpiar()
		Me.LstClientes_Enters()
		activo = False
	End Sub
	Private Sub Cargar_listas()
		Me.LstClientes.Cargar("  SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                                        FROM     Empresa INNER JOIN
                                        Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                                        Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                                        Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                                        Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                                        WHERE  (Usuarios.Usuario LIKE '%" & Inicio.LblUsuario.Text & "%')")
		Me.LstClientes.SelectItem = Inicio.Clt

	End Sub
	Private Sub Limpiar()
		Me.LblFiltro.Text = ""
		Prin = True
		If Me.TablaCat.Rows.Count > 0 Then
			Me.TablaCat.Columns.Clear()
		End If
		Me.TxtFiltro.Text = ""
	End Sub

	Private Sub LstClientes_cambio_item(value As String, texto As String) Handles LstClientes.Cambio_item
		If activo = False Then
			Buscar()
		End If
	End Sub
	Private Sub Inserta_Mayor(ByVal nivel1 As String, ByVal cuenta As String,
							  ByVal descripcion As String, ByVal cliente As Integer, ByVal Nat As String, ByVal Clasificacion As String, ByVal Letra_Contable As String, ByVal RFC As String, ByVal Bal As String, ByVal Cer As String, ByVal ccc As String, ByVal cac As String, ByVal Blnce As String, ByVal Edo As String)

		'
		Dim sql As String = ""
		sql = "INSERT INTO dbo.Catalogo_de_Cuentas "
		sql &= "("
		sql &= "Nivel1, "
		sql &= "Nivel2,"
		sql &= "Nivel3,"
		sql &= "Nivel4,"
		sql &= "Cuenta,"
		sql &= "Descripcion,"
		sql &= "Naturaleza,"
		sql &= "Clasificacion,"
		sql &= "Codigo_Agrupador,"
		If RFC <> "" Then
			sql &= "RFC,"
		Else
			sql &= "RFC,"
		End If
		sql &= "Id_Empresa , Clave, "
		sql &= "Balanza,"
		sql &= "Balance,"
		sql &= "Estado_de_Resultados,"
		sql &= "Cta_ceros,  "

		sql &= "Cta_Cargo_Cero,"
		sql &= "Cta_Abono_Cero  "


		sql &= "	)  "
		sql &= "VALUES  "
		sql &= "	(  "
		sql &= "	'" & nivel1 & "'," '@nivel1
		sql &= "	'0000'," '@nivel2
		sql &= "	'0000'," '@nivel3
		sql &= "	'0000'," '@nivel4
		sql &= "	'" & cuenta & "'," '@cuenta
		sql &= "	'" & Trim(descripcion) & "'," '@descripcion
		sql &= "	'" & Trim(Nat) & "'," '@naturaleza
		sql &= "	'" & Trim(Clasificacion) & "'," '@clasificacion
		sql &= "	'" & nivel1.ToString.Substring(0, 3) & "'," '@codigo_agrupador
		If Trim(Clasificacion) = "AFI" Then
			RFC = "Activos"
		End If


		If RFC = "" Then
			sql &= "	NULL," '@RFC
		Else
			sql &= "	'" & RFC & "'," '@RFC
		End If
		sql &= "	" & cliente & " , '" & Trim(Letra_Contable) & "'," '@Id_Empresa    
		sql &= "	" & Bal & "," '@balanza
		sql &= "	" & Blnce & "," '@Balance
		sql &= "	" & Edo & "," '@Estado
		sql &= "	" & Cer & "," '@cat_ceros
		sql &= "	" & ccc & "," '@balanza
		sql &= "	" & cac & "" '@cat_ceros
		sql &= "  )"
		' Ingresar codigo para importar catalogos
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("Catalogo_N1", sql)
		End If
	End Sub
	Private Sub Crear_cuenta(ByVal nivel1 As String, ByVal nivel2 As String, ByVal nivel3 As String,
							 ByVal nivel4 As String, ByVal cuenta As String, ByVal descripcion As String, ByVal cliente As Integer, Optional ByVal RFC As String = "")
		Dim ds As DataSet = Eventos.Obtener_DS("Select Naturaleza,Clasificacion,clave,Balanza,Balance,Estado_de_Resultados,Cta_ceros,Cta_Cargo_Cero,Cta_Abono_Cero from Catalogo_de_Cuentas where nivel1 ='" & cuenta.ToString.Substring(0, 4) & "' and Id_Empresa = " & cliente & "  ")

		If ds.Tables(0).Rows.Count > 0 Then
			Dim sql As String = ""
			sql = "INSERT INTO dbo.Catalogo_de_Cuentas "
			sql &= "("
			sql &= "Nivel1, "
			sql &= "Nivel2,"
			sql &= "Nivel3,"
			sql &= "Nivel4,"
			sql &= "Cuenta,"
			sql &= "Descripcion,"
			sql &= "Naturaleza,"
			sql &= "Clasificacion,"
			sql &= "Codigo_Agrupador,"
			If RFC <> "" Then
				sql &= "RFC,"
			Else
				sql &= "RFC,"
			End If
			sql &= "Id_Empresa ,clave, "
			sql &= "Balanza,"
			sql &= "Balance,"
			sql &= "Estado_de_Resultados,"
			sql &= "Cta_ceros,  "

			sql &= "Cta_Cargo_Cero,"
			sql &= "Cta_Abono_Cero  "

			sql &= "	)  "
			sql &= "VALUES  "
			sql &= "	(  "
			sql &= "	'" & nivel1 & "'," '@nivel1
			sql &= "	'" & nivel2 & "'," '@nivel2
			sql &= "	'" & nivel3 & "'," '@nivel3
			sql &= "	'" & nivel4 & "'," '@nivel4
			sql &= "	'" & cuenta & "'," '@cuenta
			sql &= "	'" & Trim(descripcion) & "'," '@descripcion
			sql &= "	'" & Trim(ds.Tables(0).Rows(0)("Naturaleza")) & "'," '@naturaleza
			sql &= "	'" & Trim(ds.Tables(0).Rows(0)("Clasificacion")) & "'," '@clasificacion
			Dim su As String = ""
			If (nivel4 <> "0000" Or nivel3 <> "0000") And nivel2 <> "0000" Then
				su = nivel3.Substring(2, 2)
			End If

			sql &= "'" & nivel1.ToString.Substring(0, 3) & su & "'," '@codigo_agrupador
			If Trim(ds.Tables(0).Rows(0)("Clasificacion")) = "AFI" Then
				RFC = "Activos"
			End If

			If RFC = "" Then
				sql &= "	NULL," '@RFC
			Else
				sql &= "	'" & RFC & "'," '@RFC
			End If


			sql &= "	" & cliente & ", '" & Trim(ds.Tables(0).Rows(0)("clave")) & "'," '@Id_Empresa   

			sql &= "	" & Trim(Eventos.Bool2(IIf(IsDBNull(ds.Tables(0).Rows(0)("Balanza")) = True, False, ds.Tables(0).Rows(0)("Balanza")))) & "," '@naturaleza
			sql &= "	" & Trim(Eventos.Bool2(IIf(IsDBNull(ds.Tables(0).Rows(0)("Balance")) = True, False, ds.Tables(0).Rows(0)("Balance")))) & "," '@naturaleza
			sql &= "	" & Trim(Eventos.Bool2(IIf(IsDBNull(ds.Tables(0).Rows(0)("Estado_de_Resultados")) = True, False, ds.Tables(0).Rows(0)("Estado_de_Resultados")))) & "," '@naturaleza
			sql &= "	" & Trim(Eventos.Bool2(IIf(IsDBNull(ds.Tables(0).Rows(0)("Cta_ceros")) = True, False, ds.Tables(0).Rows(0)("Cta_ceros")))) & "," '@clasificacion
			sql &= "	" & Trim(Eventos.Bool2(IIf(IsDBNull(ds.Tables(0).Rows(0)("Cta_Cargo_Cero")) = True, False, ds.Tables(0).Rows(0)("Cta_Cargo_Cero")))) & "," '@naturaleza
			sql &= "	" & Trim(Eventos.Bool2(IIf(IsDBNull(ds.Tables(0).Rows(0)("Cta_Abono_Cero")) = True, False, ds.Tables(0).Rows(0)("Cta_Abono_Cero")))) & "" '@clasificacion
			sql &= "  )"
			' Ingresar codigo para importar catalogos
			If Eventos.Comando_sql(sql) > 0 Then
				Eventos.Insertar_usuariol("Catalogo_NX", sql)
			End If
		Else

		End If
	End Sub

    Private Function Tiene_Hijos(ByVal id_cuenta As Integer)

        Dim hacer As Boolean
        Dim Contador As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("select Nivel1,Nivel2,Nivel3,Nivel4 from catalogo_de_cuentas where Id_cat_Cuentas= " & id_cuenta & " ")
        If ds.Tables(0).Rows.Count > 0 Then
            If Trim(ds.Tables(0).Rows(0)("Nivel2")) = "0000" And Trim(ds.Tables(0).Rows(0)("Nivel3")) = "0000" And Trim(ds.Tables(0).Rows(0)("Nivel4")) = "0000" Then
                ' Es la cuenta Inicio
                Contador = ObtenerValorDB("catalogo_de_cuentas", "Count(cuenta) as Cuantos ", "Nivel1= '" & Trim(ds.Tables(0).Rows(0)("Nivel1")) & "'and  Nivel2 > 0 and Id_Empresa = " & Me.LstClientes.SelectItem & " ", True)
                If Contador > 1 Then
                    hacer = False
                Else
                    hacer = True
                End If
            ElseIf Trim(ds.Tables(0).Rows(0)("Nivel3")) = "0000" And Trim(ds.Tables(0).Rows(0)("Nivel4")) = "0000" Then
                ' Es cuenta nivel 2
                Contador = ObtenerValorDB("catalogo_de_cuentas", "Count(cuenta) as Cuantos ", "Nivel1= '" & Trim(ds.Tables(0).Rows(0)("Nivel1")) & "' and Nivel2 = '" & Trim(ds.Tables(0).Rows(0)("Nivel2")) & "' and Nivel3 > 0 and Id_Empresa = " & Me.LstClientes.SelectItem & " ", True)
                If Contador > 1 Then
                    hacer = False
                Else
                    hacer = True
                End If
            ElseIf Trim(ds.Tables(0).Rows(0)("Nivel3")) <> "0000" And Trim(ds.Tables(0).Rows(0)("Nivel4")) = "0000" Then
                ' Es cuenta nivel 3
                Contador = ObtenerValorDB("catalogo_de_cuentas", "Count(cuenta) as Cuantos ",
                                          "Nivel1= '" & Trim(ds.Tables(0).Rows(0)("Nivel1")) & "' and Nivel2 = '" & Trim(ds.Tables(0).Rows(0)("Nivel2")) & "' 
                                          and Nivel3  = '" & Trim(ds.Tables(0).Rows(0)("Nivel3")) & "' and Nivel4 > 0 and Id_Empresa = " & Me.LstClientes.SelectItem & " ", True)
                If Contador > 1 Then
                    hacer = False
                Else
                    hacer = True
                End If
            Else
                hacer = True
            End If
        End If
        Return hacer
    End Function
    ''' <summary>
    ''' Esta rutina permite zxxx
    ''' </summary>
    ''' <param name="cuenta"> Es una variable de tipo String en formato  ################ </param>
    ''' <returns></returns>
    Private Function Hay_Polizas(ByVal cuenta As String)
		Dim hacer As Boolean
        Dim ds As DataSet = Eventos.Obtener_DS("Select * from Detalle_Polizas  INNER JOIN Polizas  ON Polizas .ID_poliza = Detalle_Polizas.ID_poliza INNER JOIN Empresa ON Polizas.Id_Empresa =Empresa.Id_Empresa where Detalle_Polizas.cuenta=  '" & cuenta.Replace("-", "") & "' and Empresa.Id_Empresa =  " & Me.LstClientes.SelectItem & " ")
        If ds.Tables(0).Rows.Count > 0 Then
			hacer = False
		Else
			hacer = True
		End If
		Return hacer
	End Function
	Private Function Verifica_existencia(ByVal cuenta As String)
		Dim hacer As Boolean
		Dim sql As String = " Select cuenta from catalogo_De_cuentas where cuenta= '" & cuenta & "' and Id_Empresa = " & Me.LstClientes.SelectItem & " "
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			hacer = False
		Else
			hacer = True
		End If
		Return hacer
	End Function
	Private Function Calcula_cuenta(ByVal mayor As String)
		Dim Cuenta As String = ""
		If mayor.ToString.Substring(12, 4) = "0000" And mayor.ToString.Substring(8, 4) = "0000" And mayor.ToString.Substring(4, 4) > 0 Then
			'Nivel 3
			Cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "max (Nivel3 ) + 1 ", "  Nivel1 =" & mayor.ToString.Substring(0, 4) & "  AND Nivel2 =" & mayor.ToString.Substring(4, 4) & "   AND Nivel3 >= 0 and Id_Empresa = " & Me.LstClientes.SelectItem & " ", True))
			Cuenta = Format(Cuenta).PadLeft(4, "0")
			Cuenta = mayor.Substring(0, 8) & Cuenta & "0000"
		ElseIf mayor.ToString.Substring(12, 4) = "0000" And mayor.ToString.Substring(8, 4) > 0 And mayor.ToString.Substring(4, 4) > 0 Then
			'Nivel 4
			Cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "max (Nivel4 ) + 1 ", "  Nivel1 =" & mayor.ToString.Substring(0, 4) & "  AND Nivel2 =" & mayor.ToString.Substring(4, 4) & " AND Nivel3=" & mayor.ToString.Substring(8, 4) & " AND Nivel4 >= 0000 and Id_Empresa = " & Me.LstClientes.SelectItem & " ", True))
			Cuenta = Format(Cuenta).PadLeft(4, "0")
			Cuenta = mayor.Substring(0, 12) & Cuenta

		ElseIf mayor.ToString.Substring(12, 4) = "0000" And mayor.ToString.Substring(8, 4) = "0000" And mayor.ToString.Substring(4, 4) = "0000" Then
			'Nivel2
			Cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "max (Nivel2 ) + 1 ", "  Nivel1 =" & mayor.ToString.Substring(0, 4) & " and Id_Empresa = " & Me.LstClientes.SelectItem & " ", True))
			Cuenta = Format(Cuenta).PadLeft(4, "0")
			Cuenta = mayor.Substring(0, 4) & Cuenta & "00000000"
		End If
		Return Cuenta
	End Function
	Private Sub Buscar()
		'For i As Integer = 0 To Me.TablaCatalogo.tabla.Rows.Count - 1
		'    Dim text As String = UCase(Trim(Me.TablaCatalogo.tabla.Item(6, i).Value))
		'    If text.Contains("CAJA") Then
		'        Me.TablaCatalogo.tabla.Item(6, i).Style.BackColor = Color.Red
		'    End If
		'Next


		Dim sql As String = "SELECT  	Id_cat_Cuentas, 	Nivel1,	Nivel2,	Nivel3,	
                Nivel4,		Nivel1 + '-'+	Nivel2 + '-'+	Nivel3+ '-'+ Nivel4 as Cuenta,	Descripcion,	Naturaleza,	Clasificacion,	
                Codigo_Agrupador,	Razon_Social as Cliente,	Catalogo_de_Cuentas.RFC  FROM dbo.Catalogo_de_Cuentas 
                inner join Empresa on Empresa.Id_Empresa =  catalogo_de_cuentas.Id_Empresa  
                where Catalogo_de_Cuentas.Id_Empresa = " & Me.LstClientes.SelectItem & "  Order by cuenta"

		Eventos.LlenarDataGrid_DS(Eventos.Obtener_DS(sql), Me.TablaCat)
		Me.TablaCat.Columns(0).Visible = False
		Me.TablaCat.Columns(1).Visible = False
		Me.TablaCat.Columns(2).Visible = False
		Me.TablaCat.Columns(3).Visible = False
		Me.TablaCat.Columns(4).Visible = False
		Me.TablaCat.Columns(10).Visible = False

    End Sub

	Private Sub LstClientes_Enters() Handles LstClientes.Enters
		Buscar()
	End Sub
	Private Sub TxtFiltro_TextChanged(sender As Object, e As EventArgs) Handles TxtFiltro.TextChanged
		If Me.TablaCat.Rows.Count > 0 Then
			Try

				If Me.LblFiltro.Text <> "" Then

					Dim sql As String = "SELECT  	Id_cat_Cuentas, 	Nivel1,	Nivel2,	Nivel3,	
               Nivel4,	Nivel1 + '-'+	Nivel2 + '-'+	Nivel3+ '-'+ Nivel4 as Cuenta,	Descripcion,	Naturaleza,	Clasificacion,	
               Codigo_Agrupador,	Razon_Social as Cliente,	Catalogo_de_Cuentas.RFC  FROM dbo.Catalogo_de_Cuentas inner join Empresa on Empresa.Id_Empresa =  catalogo_de_cuentas.Id_Empresa  
             where Catalogo_de_Cuentas." & Me.LblFiltro.Text & " like '%" & Me.TxtFiltro.Text & "%' and Catalogo_de_Cuentas.Id_Empresa = " & Me.LstClientes.SelectItem & " Order by cuenta "
					Eventos.LlenarDataGrid_DS(Eventos.Obtener_DS(sql), Me.TablaCat)
					Me.TablaCat.Columns(0).Visible = False
					Me.TablaCat.Columns(1).Visible = False
					Me.TablaCat.Columns(2).Visible = False
					Me.TablaCat.Columns(3).Visible = False
					Me.TablaCat.Columns(4).Visible = False
					Me.TablaCat.Columns(10).Visible = False

                End If
			Catch ex As Exception

			End Try
		End If
	End Sub

	Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
		Me.Close()
	End Sub

	Private Sub cmdActualizar_Click(sender As Object, e As EventArgs) Handles cmdActualizar.Click
		activo = True
		Cargar_listas()
		Limpiar()
		activo = False
	End Sub
	Private Sub TablaCat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TablaCat.Click
		If TablaCat.RowCount > 0 Then
			RaiseEvent Registro(TablaCat.Item(0, TablaCat.CurrentCell.RowIndex).Value.ToString)
			Me.LblFiltro.Text = Me.TablaCat.Columns(Me.TablaCat.CurrentCell.ColumnIndex).HeaderText
		End If
	End Sub

	Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")


		' Subcuenta
		Dim RFC As String = InputBox("Teclea RFC de la cuenta:", Eventos.titulo_app, "")
		Dim Cuent As String = Me.TablaCat.Item(5, Me.TablaCat.CurrentRow.Index).Value.ToString.Replace("-", "")  ' enviar parametro de la cuenta 
		Dim cuenta As String = Calcula_cuenta(Cuent) 'Calcular la nueva cuenta  dependiendo del registro seleccionado 
		Dim descripcion As String = InputBox("Teclea el Nombre de la Cuenta:", Eventos.titulo_app, "")
		If descripcion = "" Then
			RadMessageBox.Show("Debe colocar un nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
			Exit Sub
		End If


		Crear_cuenta(cuenta.Substring(0, 4), cuenta.Substring(4, 4), cuenta.Substring(8, 4), cuenta.Substring(12, 4), cuenta, RFC & " " & descripcion, Me.LstClientes.SelectItem, RFC)


	End Sub

	Private Sub EditarCuentaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditarCuentaToolStripMenuItem.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		Dim clave As String = Me.TablaCat.Item(0, Me.TablaCat.CurrentRow.Index).Value ' enviar el parametro de la cuenta a editar
		If Tiene_Hijos(clave) = True Then

			Dim descripcion As String = InputBox("Teclea el Nombre de la Cuenta:", Eventos.titulo_app, Trim(Me.TablaCat.Item(6, Me.TablaCat.CurrentRow.Index).Value))
			If descripcion = "" Then
				RadMessageBox.Show("Debe colocar un nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
				Exit Sub
			End If
			Dim Nat As String = InputBox("Teclea la Naturaleza de la cuenta:", Eventos.titulo_app, Me.TablaCat.Item(7, Me.TablaCat.CurrentRow.Index).Value)
			If Nat = "" Then
				RadMessageBox.Show("Debe colocar una Naturaleza valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
				Exit Sub
			End If
			Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT Clasificacion FROM Catalogo_de_Cuentas where   Id_Empresa = " & Me.LstClientes.SelectItem & " ORDER BY Clasificacion")
			Dim actividad(,) As String
			ReDim actividad(2, ds.Tables(0).Rows.Count + 1)
			For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
				actividad(0, i) = ds.Tables(0).Rows(i)(0)
				Debug.Print(ds.Tables(0).Rows(i)(0))
				actividad(1, i) = "0"
			Next

			With My.Forms.DialogUnaSeleccion
				.limpiar()
				.Titulo = Eventos.titulo_app
				.Texto = "Selecciona la clasificacion:"
				.MinSeleccion = 1
				.MaxSeleccion = 1
				.elementos = actividad
				.ShowDialog()
				actividad = .elementos
				If .DialogResult = Windows.Forms.DialogResult.Cancel Then
					Exit Sub
				End If
			End With

			Dim descrip As String = ""
			For i As Integer = 0 To actividad.GetLength(1)
				If actividad(1, i) = "1" Then
					descrip = actividad(0, i)
					Exit For
				End If
			Next
			Dim RFC As String = InputBox("Teclea RFC de la cuenta:", Eventos.titulo_app, Me.TablaCat.Item(11, Me.TablaCat.CurrentRow.Index).Value.ToString)
			If RFC = "" Then

			End If


            If Trim(descrip) = "AFI" Then
				RFC = "Activos"
			End If
            Dim sql As String = "  Update dbo.Catalogo_de_Cuentas   SET  	Descripcion = '" & Trim(descripcion) & "', 	Naturaleza = '" & Trim(Nat) & "',Clasificacion = '" & Trim(descrip) & "',  RFC = '" & Trim(RFC) & "', clave ='' where Id_cat_Cuentas =" & clave & " and Id_Empresa = " & Me.LstClientes.SelectItem & " "
            If Eventos.Comando_sql(sql) > 0 Then
				Eventos.Insertar_usuariol("Catalogo_EX", sql)
			End If

			Me.LstClientes_Enters()
		Else
			If RadMessageBox.Show("La cuenta tiene hijos los cambios que afecten a esta afectaran a las cuentas Hijas deseas continuar?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
				Dim cuentas As DataSet = Eventos.Obtener_DS(" select Id_cat_Cuentas from catalogo_de_cuentas where Id_Empresa = " & Me.LstClientes.SelectItem & "  and nivel1 ='" & Me.TablaCat.Item(1, Me.TablaCat.CurrentRow.Index).Value & "' and nivel2 > 0")
				Dim descripcion As String = InputBox("Teclea el Nombre de la Cuenta:", Eventos.titulo_app, Trim(Me.TablaCat.Item(6, Me.TablaCat.CurrentRow.Index).Value))
				If descripcion = "" Then
					RadMessageBox.Show("Debe colocar un nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
					Exit Sub
				End If
				Dim Nat As String = InputBox("Teclea la Naturaleza de la cuenta:", Eventos.titulo_app, Trim(Me.TablaCat.Item(7, Me.TablaCat.CurrentRow.Index).Value))
				If Nat = "" Then
					RadMessageBox.Show("Debe colocar una Naturaleza valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
					Exit Sub
				End If
				Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT Clasificacion FROM Catalogo_de_Cuentas ORDER BY Clasificacion")
				Dim actividad(,) As String
				ReDim actividad(2, ds.Tables(0).Rows.Count + 1)
				For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
					actividad(0, i) = ds.Tables(0).Rows(i)(0)
					Debug.Print(ds.Tables(0).Rows(i)(0))
					actividad(1, i) = "0"
				Next
				With My.Forms.DialogUnaSeleccion
					.limpiar()
					.Titulo = Eventos.titulo_app
					.Texto = "Selecciona la clasificacion:"
					.MinSeleccion = 1
					.MaxSeleccion = 1
					.elementos = actividad
					.ShowDialog()
					actividad = .elementos
					If .DialogResult = Windows.Forms.DialogResult.Cancel Then
						Exit Sub
					End If
				End With

				Dim descrip As String = ""
				For i As Integer = 0 To actividad.GetLength(1)
					If actividad(1, i) = "1" Then
						descrip = actividad(0, i)
						Exit For
					End If
				Next

                If cuentas.Tables(0).Rows.Count > 0 Then
                    Dim sql As String = " Update dbo.Catalogo_de_Cuentas   SET  	Descripcion = '" & Trim(descripcion) & "',	Naturaleza = '" & Trim(Nat) & "',Clasificacion = '" & Trim(descrip) & "', clave ='' where Id_cat_Cuentas =" & clave & " and Id_Empresa = " & Me.LstClientes.SelectItem & " "
                    If Eventos.Comando_sql(sql) > 0 Then
						Eventos.Insertar_usuariol("Catalogo_EX", sql)
						For i As Integer = 0 To cuentas.Tables(0).Rows.Count - 1
                            sql = "  Update dbo.Catalogo_de_Cuentas   SET  	 	Naturaleza = '" & Trim(Nat) & "',Clasificacion = '" & Trim(descrip) & "', clave ='' where Id_cat_Cuentas =" & cuentas.Tables(0).Rows(i)(0) & " and Id_Empresa = " & Me.LstClientes.SelectItem & " "
                            If Eventos.Comando_sql(sql) > 0 Then
								Eventos.Insertar_usuariol("Catalogo_EX", sql)
							End If
						Next
						Me.LstClientes_Enters()
					End If
				End If
			Else
				Exit Sub
			End If
		End If
	End Sub

	Private Sub EliminarCuentaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarCuentaToolStripMenuItem.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If RadMessageBox.Show("Realmente deseas eliminar La cuenta: " & Me.TablaCat.Item(5, Me.TablaCat.CurrentRow.Index).Value & " - " & Me.TablaCat.Item(6, Me.TablaCat.CurrentRow.Index).Value & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
			Dim clave As String = Me.TablaCat.Item(0, Me.TablaCat.CurrentRow.Index).Value
			If Tiene_Hijos(clave) = True Then
                If Hay_Polizas(Me.TablaCat.Item(5, Me.TablaCat.CurrentRow.Index).Value) Then
                    If Eventos.Comando_sql("Delete from dbo.Catalogo_de_cuentas where  Id_Empresa = " & Me.LstClientes.SelectItem & " and Id_cat_Cuentas=" & clave) > 0 Then
                        RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                        Eventos.Insertar_usuariol("Catalogo_de_cuentas_D", "Delete from dbo.Catalogo_de_cuentas where Id_cat_Cuentas= " & clave & "")
                    Else
                        RadMessageBox.Show("No se pudo eliminar el registro...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                    End If
                Else
                    RadMessageBox.Show("No se pudo eliminar el registro, existen polizas que usan la cuenta...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
				End If
			Else
				RadMessageBox.Show("No se puede eliminar la cuenta, elimina primero las cuentas Hijas...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
			End If
		End If
	End Sub

	Private Sub NuevaCuentaMadreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevaCuentaMadreToolStripMenuItem.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		Dim Cuenta As String = InputBox("Teclea la Cuenta en 4 Digitos:", Eventos.titulo_app, "")
		If Cuenta = "" Then
			RadMessageBox.Show("Debe colocar una cuenta valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
			Exit Sub
		End If
		Dim descripcion As String = InputBox("Teclea el Nombre de la Cuenta:", Eventos.titulo_app, "")
		If descripcion = "" Then
			RadMessageBox.Show("Debe colocar un nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
			Exit Sub
		End If
		Dim Nat As String = InputBox("Teclea la Naturaleza de la Cuenta:", Eventos.titulo_app, "")
		If Nat = "" Then
			RadMessageBox.Show("Debe colocar una naturaleza valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
			Exit Sub
		End If
		Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT Clasificacion FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.LstClientes.SelectItem & " ORDER BY Clasificacion")
		Dim actividad(,) As String
		ReDim actividad(2, ds.Tables(0).Rows.Count + 1)
		For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
			actividad(0, i) = ds.Tables(0).Rows(i)(0)
			Debug.Print(ds.Tables(0).Rows(i)(0))
			actividad(1, i) = "0"
		Next
		With My.Forms.DialogUnaSeleccion
			.limpiar()
			.Titulo = Eventos.titulo_app
			.Texto = "Selecciona la clasificacion:"
			.MinSeleccion = 1
			.MaxSeleccion = 1
			.elementos = actividad
			.ShowDialog()
			actividad = .elementos
			If .DialogResult = Windows.Forms.DialogResult.Cancel Then
				Exit Sub
			End If
		End With

		Dim descrip As String = ""
		For i As Integer = 0 To actividad.GetLength(1)
			If actividad(1, i) = "1" Then
				descrip = actividad(0, i)
				Exit For
			End If
		Next




        If Verifica_existencia(Cuenta & "000000000000") = True Then


            Inserta_Mayor(Cuenta, Cuenta & "000000000000", descripcion, Me.LstClientes.SelectItem, Nat, descrip, "", "", 0, 0, 0, 0, 0, 0)
        Else
			RadMessageBox.Show("La cuenta madre ya Existe...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
			Exit Sub
		End If


	End Sub

	Private Sub CmdExp_Click(sender As Object, e As EventArgs) Handles CmdExp.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.TablaCat.RowCount > 0 Then
			If Me.TablaCat.Columns.Count > 256 Then
				RadMessageBox.Show("El rango de fechas sobrepasa las columnas de una hoja de excel, disminuye el rango...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
				Exit Sub
			End If
			Dim excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcel("vacio", False)
			For col As Integer = 0 To Me.TablaCat.Columns.Count - 1
				Eventos.EscribeExcel(excel, 1, col + 1, Me.TablaCat.Columns(col).HeaderText)
			Next
			For i As Integer = 0 To Me.TablaCat.RowCount - 1
				For j As Integer = 0 To Me.TablaCat.Columns.Count - 1
					Eventos.EscribeExcel(excel, i + 1, j + 1, Valor(Me.TablaCat.Item(j, i).Value))
				Next
			Next
			Eventos.Mostrar_Excel(excel)
		Else
			RadMessageBox.Show("No hay datos para exportar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
		End If
	End Sub
    Private Sub CmdQuitar_Click(sender As Object, e As EventArgs) Handles CmdQuitar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim sql As String = "DELETE FROM Catalogo_de_Cuentas "
        sql &= " FROM Catalogo_de_Cuentas AS C"
        sql &= " INNER JOIN (SELECT  mAX(Id_cat_Cuentas)	AS Id_cat_Cuentas, c.cuenta"
        sql &= " FROM Catalogo_de_Cuentas AS c"
        sql &= " WHERE c.Cuenta IN  (SELECT Cuenta FROM ("
        sql &= " SELECT  count(cuenta) AS Cantidad , Cuenta"
        sql &= " FROM Catalogo_de_Cuentas "
        sql &= " where  Id_Empresa = " & Me.LstClientes.SelectItem & " "
        sql &= " GROUP BY Cuenta) AS Tab WHERE Cantidad >1) "
        sql &= " GROUP BY Cuenta   ) as Ctas"
        sql &= "  ON Ctas.Id_cat_Cuentas = c.Id_cat_Cuentas"
        sql &= " WHERE C.Id_Empresa = " & Me.LstClientes.SelectItem & " AND  C.Id_cat_Cuentas = Ctas.Id_cat_Cuentas"
        If Eventos.Comando_sql(sql) > 0 Then
            Buscar()
            RadMessageBox.Show("Proceso terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Else
            RadMessageBox.Show("No hay cuentas duplicadas", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub

    Private Sub CmdSaldos_Click(sender As Object, e As EventArgs) Handles CmdSaldos.Click
        Cuentas_con_saldo.Id_Empresa = Me.LstClientes.SelectItem
        Eventos.Abrir_form(Cuentas_con_saldo)
    End Sub
End Class