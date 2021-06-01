Imports Telerik.WinControls
Public Class Polizas_Modelo_Recibidas
    Dim NuevoF, NuevoN, NuevoC As Boolean
    Public Event Registro(ByVal clave As String)
    Private Sub Polizas_Modelo_Recibidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_listas()
        Estilo()
    End Sub
    Private Sub Estilo()
        Eventos.DiseñoTablaEnca(Me.TablaFactura)
        Eventos.DiseñoTablaEnca(Me.TablaNotasC)
        Eventos.DiseñoTablaEnca(Me.TablaComplemento)
    End Sub
    Private Sub Cargar_listas()
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
    End Sub


    Private Sub CmdLimpiarF_Click(sender As Object, e As EventArgs) Handles CmdLimpiarF.Click
        If Me.TablaFactura.Rows.Count > 0 Then
            LimpiaFacturas()
            Me.LetraFacturas.Items.Clear()
            Me.lstCliente.SelectText = ""
        End If
    End Sub
    Private Sub LimpiaFacturas()
        Me.TablaFactura.Rows.Clear()
    End Sub
    Private Sub LimpiaNotas()
        Me.TablaNotasC.Rows.Clear()
    End Sub
    Private Sub LimpiaComplemento()
        Me.TablaComplemento.Rows.Clear()
    End Sub

    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub
    Private Sub CmdLimpiarN_Click(sender As Object, e As EventArgs) Handles CmdLimpiarN.Click
        If Me.TablaNotasC.Rows.Count > 0 Then
            LimpiaNotas()
            Me.LetrasNotas.Items.Clear()
        End If
    End Sub
    Private Sub CmdLimpiarC_Click(sender As Object, e As EventArgs) Handles CmdLimpiarC.Click
        If Me.TablaComplemento.Rows.Count > 0 Then
            LimpiaComplemento()
            Me.LetraComplementos.Items.Clear()
        End If
    End Sub

    Private Function Candado(ByVal I As Integer)
        Dim Hacer As Boolean
        If Me.TablaFactura.Item(FDespuesFacturas.Index, I).Value = Nothing And Me.TablaFactura.Item(FantesFacturas.Index, I).Value = Nothing Then
            Hacer = False
        Else
            If Me.TablaFactura.Item(TpolFactura.Index, I).Value = Nothing Then
                Hacer = False
            Else
                If Me.TablaFactura.Item(LetraFacturas.Index, I).Value = Nothing Then
                    Hacer = False
                Else
                    If Me.TablaFactura.Item(TransfFactura.Index, I).Value = True Then
                        Me.TablaFactura.Item(ChequeFacura.Index, I).Value = False
                        Me.TablaFactura.Item(EfecFacturas.Index, I).Value = False
                        Me.TablaFactura.Item(ProvFacturas.Index, I).Value = False
                        Me.TablaFactura.Item(AnticipoFactura.Index, I).Value = False

                        If Me.TablaFactura.Item(BOFactura.Index, I).Value = Nothing Then
                            Hacer = False
                        Else
                            If Me.TablaFactura.Item(BDFactura.Index, I).Value = Nothing Then
                                Hacer = False
                            Else
                                Hacer = True
                                Me.TablaFactura.Item(SelFactura.Index, I).Value = True
                            End If
                        End If
                    ElseIf Me.TablaFactura.Item(EfecFacturas.Index, I).Value = True Then
                        Me.TablaFactura.Item(TransfFactura.Index, I).Value = False
                        Me.TablaFactura.Item(ChequeFacura.Index, I).Value = False
                        Me.TablaFactura.Item(ProvFacturas.Index, I).Value = False
                        Me.TablaFactura.Item(AnticipoFactura.Index, I).Value = False
                        Hacer = True
                        Me.TablaFactura.Item(SelFactura.Index, I).Value = True
                    ElseIf Me.TablaFactura.Item(ChequeFacura.Index, I).Value = True Then
                        Me.TablaFactura.Item(EfecFacturas.Index, I).Value = False
                        Me.TablaFactura.Item(TransfFactura.Index, I).Value = False
                        Me.TablaFactura.Item(ProvFacturas.Index, I).Value = False
                        Me.TablaFactura.Item(AnticipoFactura.Index, I).Value = False
                        If Me.TablaFactura.Item(BOCHFactura.Index, I).Value = Nothing Then
                            Hacer = False
                        Else
                            Hacer = True
                            Me.TablaFactura.Item(SelFactura.Index, I).Value = True
                        End If
                    ElseIf Me.TablaFactura.Item(ProvFacturas.Index, I).Value = True Then
                        Me.TablaFactura.Item(EfecFacturas.Index, I).Value = False
                        Me.TablaFactura.Item(TransfFactura.Index, I).Value = False
                        Me.TablaFactura.Item(ChequeFacura.Index, I).Value = False
                        Me.TablaFactura.Item(AnticipoFactura.Index, I).Value = False
                        Hacer = True
                        Me.TablaFactura.Item(SelFactura.Index, I).Value = True
                    ElseIf Me.TablaFactura.Item(AnticipoFactura.Index, I).Value = True Then
                        Me.TablaFactura.Item(EfecFacturas.Index, I).Value = False
                        Me.TablaFactura.Item(TransfFactura.Index, I).Value = False
                        Me.TablaFactura.Item(ChequeFacura.Index, I).Value = False
                        Me.TablaFactura.Item(ProvFacturas.Index, I).Value = False
                        Hacer = True
                        Me.TablaFactura.Item(SelFactura.Index, I).Value = True
                    End If
                End If
            End If
        End If
        Return Hacer
    End Function
    Private Function CandadoN(ByVal I As Integer)
        Dim Hacer As Boolean
        If Me.TablaNotasC.Item(FechaDNotas.Index, I).Value = Nothing And Me.TablaNotasC.Item(FantesNotas.Index, I).Value = Nothing Then
            Hacer = False
        Else
            If Me.TablaNotasC.Item(TPOLNotas.Index, I).Value = Nothing Then
                Hacer = False
            Else
                If Me.TablaNotasC.Item(LetrasNotas.Index, I).Value = Nothing Then
                    Hacer = False
                Else
                    If Me.TablaNotasC.Item(TransfNotas.Index, I).Value = True Then
                        Me.TablaNotasC.Item(ChequeNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(EfeNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(PPNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(PANotas.Index, I).Value = False


                        Hacer = True
                        Me.TablaNotasC.Item(SelNotas.Index, I).Value = True

                    ElseIf Me.TablaNotasC.Item(EfeNotas.Index, I).Value = True Then
                        Me.TablaNotasC.Item(TransfNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(ChequeNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(PPNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(PANotas.Index, I).Value = False
                        Hacer = True
                        Me.TablaNotasC.Item(SelNotas.Index, I).Value = True
                    ElseIf Me.TablaNotasC.Item(ChequeNotas.Index, I).Value = True Then
                        Me.TablaNotasC.Item(EfeNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(TransfNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(PPNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(PANotas.Index, I).Value = False
                        Hacer = True
                        Me.TablaNotasC.Item(SelNotas.Index, I).Value = True
                    ElseIf Me.TablaNotasC.Item(PPNotas.Index, I).Value = True Then
                        Me.TablaNotasC.Item(EfeNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(TransfNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(ChequeNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(PANotas.Index, I).Value = False
                        Hacer = True
                        Me.TablaNotasC.Item(SelNotas.Index, I).Value = True
                    ElseIf Me.TablaNotasC.Item(PANotas.Index, I).Value = True Then
                        Me.TablaNotasC.Item(EfeNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(TransfNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(ChequeNotas.Index, I).Value = False
                        Me.TablaNotasC.Item(PPNotas.Index, I).Value = False
                        Hacer = True
                        Me.TablaNotasC.Item(SelNotas.Index, I).Value = True
                    End If
                End If
            End If
        End If
        Return Hacer
    End Function
    Private Sub Guardar_NuevoF(ByVal RFC As String, ByVal Nombre As String, ByVal FechaAntesde As String,
                        ByVal FechaDespuesde As String, ByVal Clave As String, ByVal Efectivo As String, ByVal Transferencia As String,
                        ByVal Banco_Origen As String, ByVal Banco_Destino As String, ByVal Cheque As String, ByVal Nom_Banco_Ch As String,
                          ByVal Tipo_Poliza As String, ByVal Provision As String, ByVal Anticipo As String)
        Dim sql As String = "INSERT INTO dbo.Facturas_Poliza_Modelo
	(	RFC,	Nombre,	FechaAntesde,	FechaDespuesde,	Clave,	Efectivo,	Transferencia,	Banco_Origen,	Banco_Destino,	Cheque,
	Nom_Banco_Ch,	Tipo_Poliza,	Provision,	Anticipo,Id_Empresa , Emitidas	) VALUES 	(	
	'" & RFC & "',	'" & Nombre & "',	'" & FechaAntesde & "',	'" & FechaDespuesde & "',	'" & Clave & "',	'" & Efectivo & "',	'" & Transferencia & "',
	'" & Banco_Origen & "',	'" & Banco_Destino & "',	'" & Cheque & "',	'" & Nom_Banco_Ch & "',	'" & Tipo_Poliza & "',	'" & Provision & "',	'" & Anticipo & "', " & Me.lstCliente.SelectItem & ",0)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InserPolizMF", sql)
        End If
    End Sub
    Private Sub Guardar_NuevoN(ByVal RFC As String, ByVal Nombre As String, ByVal FechaAntesde As String,
                        ByVal FechaDespuesde As String, ByVal Clave As String, ByVal Efectivo As String, ByVal Transferencia As String,
                       ByVal Cheque As String, ByVal Tipo_Poliza As String, ByVal ProvisionAcred As String, ByVal ProvisionProveed As String)
        Dim sql As String = "INSERT INTO dbo.Notas_Pol_Modelo
	(	RFC,	Nombre,	FechaAntesde,	FechaDespuesde,	Clave,	Efectivo,	Transferencia,		Cheque,	Tipo_Poliza,	ProvisionAcred,	ProvisionProveed,Id_Empresa, Emitidas	) VALUES 	(	
	'" & RFC & "',	'" & Nombre & "',	'" & FechaAntesde & "',	'" & FechaDespuesde & "',	'" & Clave & "',	'" & Efectivo & "',	'" & Transferencia & "',
	'" & Cheque & "',	'" & Tipo_Poliza & "',	'" & ProvisionAcred & "',	'" & ProvisionProveed & "'," & Me.lstCliente.SelectItem & ",0	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InserPolizMN", sql)
        End If
    End Sub
    Private Sub Guardar_NuevoC(ByVal RFC As Integer, ByVal Nombre As Integer, ByVal FechaAntesde As String,
                       ByVal FechaDespuesde As String, ByVal Clave As String, ByVal Efectivo As String, ByVal Transferencia As String,
                       ByVal Banco_Origen As String, ByVal Banco_Destino As String, ByVal Cheque As String, ByVal Nom_Banco_Ch As String,
                         ByVal Tipo_Poliza As String, ByVal Anticipo As String)
        Dim sql As String = "INSERT INTO dbo.Complementos_Pol_Modelo
	(	RFC,	Nombre,	FechaAntesde,	FechaDespuesde,	Clave,	Efectivo,	Transferencia,	Banco_Origen,	Banco_Destino,	Cheque,
	Nom_Banco_Ch,	Tipo_Poliza,	 	Anticipo	) VALUES 	(		'" & RFC & "',	'" & Nombre & "',	'" & FechaAntesde & "',	'" & FechaDespuesde & "',	'" & Clave & "',	'" & Efectivo & "',	'" & Transferencia & "',
	'" & Banco_Origen & "',	'" & Banco_Destino & "',	'" & Cheque & "',	'" & Nom_Banco_Ch & "',	'" & Tipo_Poliza & "','" & Anticipo & "'	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InserPolizMC", sql)
        End If
    End Sub

    Private Sub Guardar_Edision_F(ByVal RFC As String, ByVal Nombre As String, ByVal FechaAntesde As String,
                        ByVal FechaDespuesde As String, ByVal Clave As String, ByVal Efectivo As String, ByVal Transferencia As String,
                        ByVal Banco_Origen As String, ByVal Banco_Destino As String, ByVal Cheque As String, ByVal Nom_Banco_Ch As String,
                          ByVal Tipo_Poliza As String, ByVal Provision As String, ByVal Anticipo As String, ByVal Id As Integer)


        Dim sql As String = "
            UPDATE dbo.Facturas_Poliza_Modelo
                    SET 
	                    RFC = '" & RFC & "',
	                    Nombre = '" & Nombre & "',
	                    FechaAntesde = '" & FechaAntesde & "',
	                    FechaDespuesde = '" & FechaDespuesde & "',
	                    Clave = '" & Clave & "',
	                    Efectivo = '" & Efectivo & "',
	                    Transferencia = '" & Transferencia & "',
	                    Banco_Origen = '" & Banco_Origen & "',
	                    Banco_Destino = '" & Banco_Destino & "',
	                    Cheque = '" & Cheque & "',
	                    Nom_Banco_Ch = '" & Nom_Banco_Ch & "',
	                    Tipo_Poliza = '" & Tipo_Poliza & "',
	                    Provision = '" & Provision & "',
	                    Anticipo = '" & Anticipo & "'  where Id_Pol_Mod_Factura = " & Id & ""
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InserPolizMN", sql)
        End If

    End Sub
    Private Sub Guardar_Edision_C(ByVal RFC As String, ByVal Nombre As String, ByVal FechaAntesde As String,
                        ByVal FechaDespuesde As String, ByVal Clave As String, ByVal Efectivo As String, ByVal Transferencia As String,
                        ByVal Banco_Origen As String, ByVal Banco_Destino As String, ByVal Cheque As String, ByVal Nom_Banco_Ch As String,
                          ByVal Tipo_Poliza As String, ByVal Provision As String, ByVal Anticipo As String, ByVal Id As Integer)


        Dim sql As String = "
            UPDATE dbo.Complementos_Pol_Modelo
                    SET 
	                    RFC = '" & RFC & "',
	                    Nombre = '" & Nombre & "',
	                    FechaAntesde = '" & FechaAntesde & "',
	                    FechaDespuesde = '" & FechaDespuesde & "',
	                    Clave = '" & Clave & "',
	                    Efectivo = '" & Efectivo & "',
	                    Transferencia = '" & Transferencia & "',
	                    Banco_Origen = '" & Banco_Origen & "',
	                    Banco_Destino = '" & Banco_Destino & "',
	                    Cheque = '" & Cheque & "',
	                    Nom_Banco_Ch = '" & Nom_Banco_Ch & "',
	                    Tipo_Poliza = '" & Tipo_Poliza & "',
	                  	Anticipo = '" & Anticipo & "'  where Id_Pol_Mod_Complemento = " & Id & ""
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InserPolizMC", sql)
        End If


    End Sub
    Private Sub Guardar_Edision_N(ByVal RFC As String, ByVal Nombre As String, ByVal FechaAntesde As String,
                        ByVal FechaDespuesde As String, ByVal Clave As String, ByVal Efectivo As String, ByVal Transferencia As String,
                       ByVal Cheque As String, ByVal Tipo_Poliza As String, ByVal ProvisionAcred As String, ByVal ProvisionProveed As String, ByVal Id As Integer)

        Dim sql As String = "
            UPDATE dbo.Notas_Pol_Modelo
                    SET 
	                    RFC = '" & RFC & "',
	                    Nombre = '" & Nombre & "',
	                    FechaAntesde = '" & FechaAntesde & "',
	                    FechaDespuesde = '" & FechaDespuesde & "',
	                    Clave = '" & Clave & "',
	                    Efectivo = '" & Efectivo & "',
	                    Transferencia = '" & Transferencia & "',
	                    Cheque = '" & Cheque & "',
	                    Tipo_Poliza = '" & Tipo_Poliza & "',
	                  	ProvisionAcred = '" & ProvisionAcred & "',	ProvisionProveed = '" & ProvisionProveed & "'  where Id_Pol_Mod_Nota = " & Id & " and Id_Empresa = " & Me.lstCliente.SelectItem & ""
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InserPolizMN", sql)
        End If
    End Sub

    Private Sub CmdGuardarN_Click(sender As Object, e As EventArgs) Handles CmdGuardarN.Click
        For i As Integer = 0 To Me.TablaNotasC.Rows.Count - 1
            If Me.TablaNotasC.Item(IDnOTA.Index, i).Value <> Nothing Then
                If CandadoN(i) = True Then
                    Guardar_Edision_N(Me.TablaNotasC.Item(RFCNotas.Index, i).Value, Me.TablaNotasC.Item(NombreNotas.Index, i).Value, IIf(Len(Me.TablaNotasC.Item(FantesNotas.Index, i).Value) = 1, "0" & Me.TablaNotasC.Item(FantesNotas.Index, i).Value, Me.TablaNotasC.Item(FantesNotas.Index, i).Value).ToString.Substring(0, 2),
                IIf(Len(Me.TablaNotasC.Item(FechaDNotas.Index, i).Value) = 1, "0" & Me.TablaNotasC.Item(FechaDNotas.Index, i).Value, Me.TablaNotasC.Item(FechaDNotas.Index, i).Value).ToString.Substring(0, 2), Me.TablaNotasC.Item(LetrasNotas.Index, i).Value, Eventos.Bool2(Me.TablaNotasC.Item(EfeNotas.Index, i).Value), Eventos.Bool2(Me.TablaNotasC.Item(TransfNotas.Index, i).Value),
                           Eventos.Bool2(Me.TablaNotasC.Item(ChequeNotas.Index, i).Value), Me.TablaNotasC.Item(TPOLNotas.Index, i).Value, Eventos.Bool2(Me.TablaNotasC.Item(PANotas.Index, i).Value), Eventos.Bool2(Me.TablaNotasC.Item(PPNotas.Index, i).Value), Me.TablaNotasC.Item(IDnOTA.Index, i).Value)
                End If
            Else
                If CandadoN(i) = True Then
                    Guardar_NuevoN(Me.TablaNotasC.Item(RFCNotas.Index, i).Value, Me.TablaNotasC.Item(NombreNotas.Index, i).Value, IIf(Len(Me.TablaNotasC.Item(FantesNotas.Index, i).Value) = 1, "0" & Me.TablaNotasC.Item(FantesNotas.Index, i).Value, Me.TablaNotasC.Item(FantesNotas.Index, i).Value).ToString.Substring(0, 2),
                                  IIf(Len(Me.TablaNotasC.Item(FechaDNotas.Index, i).Value) = 1, "0" & Me.TablaNotasC.Item(FechaDNotas.Index, i).Value, Me.TablaNotasC.Item(FechaDNotas.Index, i).Value).ToString.Substring(0, 2), Me.TablaNotasC.Item(LetrasNotas.Index, i).Value, Eventos.Bool2(Me.TablaNotasC.Item(EfeNotas.Index, i).Value), Eventos.Bool2(Me.TablaNotasC.Item(TransfNotas.Index, i).Value),
                           Eventos.Bool2(Me.TablaNotasC.Item(ChequeNotas.Index, i).Value), Me.TablaNotasC.Item(TPOLNotas.Index, i).Value, Eventos.Bool2(Me.TablaNotasC.Item(PANotas.Index, i).Value), Eventos.Bool2(Me.TablaNotasC.Item(PPNotas.Index, i).Value))
                End If
            End If
        Next
    End Sub
    Private Sub CmdGuardarC_Click(sender As Object, e As EventArgs) Handles CmdGuardarC.Click
        If NuevoC = True Then
            'Guardar_NuevoC(ByVal RFC As Integer, ByVal Nombre As Integer, ByVal FechaAntesde As String,
            '           ByVal FechaDespuesde As String, ByVal Clave As String, ByVal Efectivo As String, ByVal Transferencia As String,
            '           ByVal Banco_Origen As String, ByVal Banco_Destino As String, ByVal Cheque As String, ByVal Nom_Banco_Ch As String,
            '             ByVal Tipo_Poliza As String, ByVal Anticipo As String)
        Else
            'Guardar_Edision_C(ByVal RFC As Integer, ByVal Nombre As Integer, ByVal FechaAntesde As String,
            '            ByVal FechaDespuesde As String, ByVal Clave As String, ByVal Efectivo As String, ByVal Transferencia As String,
            '            ByVal Banco_Origen As String, ByVal Banco_Destino As String, ByVal Cheque As String, ByVal Nom_Banco_Ch As String,
            '              ByVal Tipo_Poliza As String, ByVal Provision As String, ByVal Anticipo As String, ByVal Id As Integer)
        End If
    End Sub
    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        'If NuevoF = True Then
        For i As Integer = 0 To Me.TablaFactura.Rows.Count - 1
            If Me.TablaFactura.Item(IdFac.Index, i).Value <> Nothing Then
                If Candado(i) = True Then
                    Guardar_Edision_F(Me.TablaFactura.Item(RFCFacturas.Index, i).Value, Me.TablaFactura.Item(NomFactura.Index, i).Value, IIf(Len(Me.TablaFactura.Item(FantesFacturas.Index, i).Value) = 1, "0" & Me.TablaFactura.Item(FantesFacturas.Index, i).Value, Me.TablaFactura.Item(FantesFacturas.Index, i).Value).ToString.Substring(0, 2),
                                  IIf(Len(Me.TablaFactura.Item(FDespuesFacturas.Index, i).Value) = 1, "0" & Me.TablaFactura.Item(FDespuesFacturas.Index, i).Value, Me.TablaFactura.Item(FDespuesFacturas.Index, i).Value).ToString.Substring(0, 2), Me.TablaFactura.Item(LetraFacturas.Index, i).Value, Eventos.Bool2(Me.TablaFactura.Item(EfecFacturas.Index, i).Value), Eventos.Bool2(Me.TablaFactura.Item(TransfFactura.Index, i).Value),
                                    Me.TablaFactura.Item(BOFactura.Index, i).Value, Me.TablaFactura.Item(BDFactura.Index, i).Value, Eventos.Bool2(Me.TablaFactura.Item(ChequeFacura.Index, i).Value), Me.TablaFactura.Item(BOCHFactura.Index, i).Value,
                                     Me.TablaFactura.Item(TpolFactura.Index, i).Value, Eventos.Bool2(Me.TablaFactura.Item(ProvFacturas.Index, i).Value), Eventos.Bool2(Me.TablaFactura.Item(AnticipoFactura.Index, i).Value), Me.TablaFactura.Item(IdFac.Index, i).Value)
                End If
            Else
                If Candado(i) = True Then
                    Guardar_NuevoF(Me.TablaFactura.Item(RFCFacturas.Index, i).Value, Me.TablaFactura.Item(NomFactura.Index, i).Value, IIf(Len(Me.TablaFactura.Item(FantesFacturas.Index, i).Value) = 1, "0" & Me.TablaFactura.Item(FantesFacturas.Index, i).Value, Me.TablaFactura.Item(FantesFacturas.Index, i).Value).ToString.Substring(0, 2),
                                  IIf(Len(Me.TablaFactura.Item(FDespuesFacturas.Index, i).Value) = 1, "0" & Me.TablaFactura.Item(FDespuesFacturas.Index, i).Value, Me.TablaFactura.Item(FDespuesFacturas.Index, i).Value).ToString.Substring(0, 2), Me.TablaFactura.Item(LetraFacturas.Index, i).Value, Eventos.Bool2(Me.TablaFactura.Item(EfecFacturas.Index, i).Value), Eventos.Bool2(Me.TablaFactura.Item(TransfFactura.Index, i).Value),
                                    Me.TablaFactura.Item(BOFactura.Index, i).Value, Me.TablaFactura.Item(BDFactura.Index, i).Value, Eventos.Bool2(Me.TablaFactura.Item(ChequeFacura.Index, i).Value), Me.TablaFactura.Item(BOCHFactura.Index, i).Value,
                                     Me.TablaFactura.Item(TpolFactura.Index, i).Value, Eventos.Bool2(Me.TablaFactura.Item(ProvFacturas.Index, i).Value), Eventos.Bool2(Me.TablaFactura.Item(AnticipoFactura.Index, i).Value))
                End If
            End If
        Next
        Me.CmdBuscarFact.PerformClick()
    End Sub

    Private Sub CmdNuevoC_Click(sender As Object, e As EventArgs) Handles CmdNuevoC.Click
        NuevoC = True
        If Me.TablaComplemento.Rows.Count > 0 Then
            Me.TablaComplemento.Rows.Clear()
        Else
            Me.TablaComplemento.Rows.Add()
            ContabilizacionComplementos()
            Cargar_bancosDevC()
            Cargar_bancosC("Transf")
            Cargar_bancosC("Ch")
        End If
    End Sub

    Private Sub CmdNuevoN_Click(sender As Object, e As EventArgs) Handles CmdNuevoN.Click
        NuevoN = True
        If Me.TablaNotasC.Rows.Count > 0 Then
            Me.TablaNotasC.Rows.Clear()
        Else
            Me.TablaNotasC.Rows.Add()
            ContabilizacionNotas()
        End If
    End Sub


    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        NuevoF = True
        If Me.TablaFactura.Rows.Count > 0 Then
            Me.TablaFactura.Rows.Clear()
        Else
            Me.TablaFactura.Rows.Add()
            ContabilizacionFacturas()
            ' Cargar_bancosDevF()
            Cargar_bancos("Transf")
            Cargar_bancos("Ch")
        End If
    End Sub

    Private Sub CmdBuscarC_Click(sender As Object, e As EventArgs) Handles CmdBuscarC.Click
        If Me.lstCliente.SelectText <> "" Then
            If Me.TablaComplemento.Rows.Count > 0 Then
                Me.TablaComplemento.Rows.Clear()
            End If
            BuscarC(Me.lstCliente.SelectItem)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        If Me.lstCliente.SelectText <> "" Then
            If Me.TablaFactura.Rows.Count > 0 Then
                Me.TablaFactura.Rows.Clear()
            End If
            BuscarF(Me.lstCliente.SelectItem)
            Color_Columnas()
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Sub Color_Columnas()
        Dim contador As Integer = 0
        For Each Column As DataGridViewColumn In TablaFactura.Columns
            If Column.Index = LetraFacturas.Index Or Column.Index = TpolFactura.Index Then
                Column.DefaultCellStyle.BackColor = Color.RoyalBlue
            ElseIf Column.Index = BOFactura.Index Or Column.Index = BDFactura.Index Then
                Column.DefaultCellStyle.BackColor = Color.GreenYellow
            ElseIf Column.Index = FantesComplementos.Index Then
                Column.DefaultCellStyle.BackColor = Color.LawnGreen
            ElseIf Column.Index = FDespuesFacturas.Index Then
                Column.DefaultCellStyle.BackColor = Color.MediumPurple
            End If
        Next
    End Sub
    Private Sub Color_Columnas_Notas()
        Dim contador As Integer = 0
        For Each Column As DataGridViewColumn In TablaNotasC.Columns
            If Column.Index = LetrasNotas.Index Or Column.Index = TPOLNotas.Index Then
                Column.DefaultCellStyle.BackColor = Color.RoyalBlue
            ElseIf Column.Index = TransfNotas.Index Then
                Column.DefaultCellStyle.BackColor = Color.GreenYellow
            ElseIf Column.Index = FantesNotas.Index Then
                Column.DefaultCellStyle.BackColor = Color.LawnGreen
            ElseIf Column.Index = FechaDNotas.Index Then
                Column.DefaultCellStyle.BackColor = Color.MediumPurple
            End If
        Next
    End Sub
    Private Sub CmdBuscarN_Click(sender As Object, e As EventArgs) Handles CmdBuscarN.Click
        If Me.lstCliente.SelectText <> "" Then
            If Me.TablaNotasC.Rows.Count > 0 Then
                Me.TablaNotasC.Rows.Clear()
            End If
            BuscarN(Me.lstCliente.SelectItem)
            Color_Columnas_Notas()
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Sub BuscarF(ByVal Cliente As Integer)
        Dim posicion As Integer = 0
        Dim Sql As String = " SELECT  Id_Pol_Mod_Factura,	RFC,	Nombre,	FechaAntesde,	FechaDespuesde,	Clave,	Efectivo,	Transferencia,	Banco_Origen,	Banco_Destino,	
                               Cheque,	Nom_Banco_Ch,	Tipo_Poliza,	Provision,	Anticipo FROM dbo.Facturas_Poliza_Modelo where Id_Empresa = " & Cliente & " "

        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaFactura.RowCount = 1
            Dim contab As DataSet = Eventos.Obtener_DS(" Select Rtrim(Clave) as Clave from ClaveEgresos where ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & "  ")
            'Dim contab As DataSet = Eventos.Obtener_DS(" Select Rtrim(Clave) as Clave from Letras_Contabilidad where Letras_Contabilidad.Id_Empresa = " & Me.lstCliente.SelectItem & "  ")
            If contab.Tables(0).Rows.Count > 0 Then
                If Me.LetraFacturas.Items.Count = 0 Then

                    For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                        Me.LetraFacturas.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                    Next
                Else
                    Me.LetraFacturas.Items.Clear()
                    For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                        Me.LetraFacturas.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                    Next
                End If
            End If
            Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
            If Tipo.Tables(0).Rows.Count > 0 Then
                If Me.TpolFactura.Items.Count = 0 Then
                    For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                        Me.TpolFactura.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                    Next
                Else
                    Me.TpolFactura.Items.Clear()
                    For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                        Me.TpolFactura.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                    Next
                End If
            End If

            Cargar_bancos("Transf")
            Cargar_bancos("Ch")
            Me.TablaFactura.RowCount = ds.Tables(0).Rows.Count
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.TablaFactura.Rows(i)
                Me.TablaFactura.Item(IdFac.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Id_Pol_Mod_Factura"))
                Me.TablaFactura.Item(RFCFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("RFC"))
                Me.TablaFactura.Item(NomFactura.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Nombre"))
                Me.TablaFactura.Item(FantesFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("FechaAntesde"))
                Me.TablaFactura.Item(FDespuesFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("FechaDespuesde"))
                Try
                    If Trim(ds.Tables(0).Rows(i)("Clave")) <> "" Then
                        Fila.Cells(LetraFacturas.Index).Value = Me.LetraFacturas.Items(Obtener_index(Trim(ds.Tables(0).Rows(i)("Clave"))))
                        '     Me.TablaImportar.Item(ContabilizacionC.INDEX, j).Value = Me.ContabilizacionC.Items(Obtener_index(ds.Tables(0).Rows(j)("Clave")))
                    End If

                Catch ex As Exception

                End Try
                Me.TablaFactura.Item(LetraFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Clave"))
                Me.TablaFactura.Item(EfecFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Efectivo"))
                Me.TablaFactura.Item(TransfFactura.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Transferencia"))
                Try
                    If Trim(ds.Tables(0).Rows(i)("Banco_Origen")) <> "" Then
                        Fila.Cells(BOFactura.Index).Value = Me.BOFactura.Items(Obtener_indexB(Trim(ds.Tables(0).Rows(i)("Banco_Origen"))))
                    End If
                Catch ex As Exception

                End Try

                Me.TablaFactura.Item(BDFactura.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Banco_Destino"))
                Me.TablaFactura.Item(ChequeFacura.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Cheque"))


                Try
                    If Trim(ds.Tables(0).Rows(i)("Nom_Banco_Ch")) <> "" Then
                        Fila.Cells(BOCHFactura.Index).Value = Me.BOCHFactura.Items(Obtener_indexbN(Trim(ds.Tables(0).Rows(i)("Nom_Banco_Ch"))))
                    End If
                Catch ex As Exception

                End Try

                Try
                    If Trim(ds.Tables(0).Rows(i)("Tipo_Poliza")) <> "" Then
                        Fila.Cells(TpolFactura.Index).Value = Me.TpolFactura.Items(Obtener_index2(Trim(ds.Tables(0).Rows(i)("Tipo_Poliza"))))
                    End If

                Catch ex As Exception

                End Try
                Me.TablaFactura.Item(ProvFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Provision"))
                Me.TablaFactura.Item(AnticipoFactura.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Anticipo"))

                posicion = posicion + 1

            Next
            Dim Emitidas As String = " SELECT DISTINCT Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor  FROM Xml_Sat WHERE tipo = 'Factura' and Id_Empresa =" & Me.lstCliente.SelectItem & " and  Emitidas = 0 and Xml_Sat.RFC_Emisor not in (SELECT Facturas_Poliza_Modelo.RFC FROM     
                                                Facturas_Poliza_Modelo   WHERE   Emitidas =0  and Id_Empresa =" & Me.lstCliente.SelectItem & ") "
            Dim em As DataSet = Eventos.Obtener_DS(Emitidas)
            If em.Tables(0).Rows.Count > 0 Then
                Me.TablaFactura.RowCount = Me.TablaFactura.RowCount + em.Tables(0).Rows.Count
                For j As Integer = 0 To em.Tables(0).Rows.Count - 1
                    Me.TablaFactura.Item(RFCFacturas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("RFC_Emisor"))
                    Me.TablaFactura.Item(NomFactura.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("Nombre_Emisor"))
                    posicion = posicion + 1
                Next
            End If

            'frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Else

            Dim Emitidas As String = " SELECT DISTINCT Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor  FROM Xml_Sat WHERE tipo = 'Factura' and  Id_Empresa =" & Me.lstCliente.SelectItem & " and Emitidas = 0 and Xml_Sat.RFC_Emisor not in (SELECT Facturas_Poliza_Modelo.RFC FROM     
                                                Facturas_Poliza_Modelo   WHERE  Emitidas = 0  and Id_Empresa =" & Me.lstCliente.SelectItem & ") "
            Dim em As DataSet = Eventos.Obtener_DS(Emitidas)
            If em.Tables(0).Rows.Count > 0 Then
                Me.TablaFactura.RowCount = Me.TablaFactura.RowCount + em.Tables(0).Rows.Count
                ContabilizacionFacturas()
                Cargar_bancos("Transf")
                Cargar_bancos("Ch")

                For j As Integer = 0 To em.Tables(0).Rows.Count - 1
                    Me.TablaFactura.Item(RFCFacturas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("RFC_Emisor"))
                    Me.TablaFactura.Item(NomFactura.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("Nombre_Emisor"))
                    posicion = posicion + 1
                Next
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If

        End If
    End Sub
    Private Sub BuscarN(ByVal Cliente As Integer)
        Dim posicion As Integer = 0
        Dim Sql As String = " SELECT 	Id_Pol_Mod_Nota,	RFC,	Nombre,	FechaAntesde,	FechaDespuesde,	Clave,	Efectivo,	
                                Transferencia,	Cheque,	Tipo_Poliza,	ProvisionAcred,	ProvisionProveed,	Id_Empresa FROM dbo.Notas_Pol_Modelo where Id_Empresa = " & Cliente & " "

        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaNotasC.RowCount = ds.Tables(0).Rows.Count

            ContabilizacionNotas()

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.TablaNotasC.Rows(i)
                Me.TablaNotasC.Item(IDnOTA.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Id_Pol_Mod_Nota"))
                Me.TablaNotasC.Item(RFCNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("RFC"))
                Me.TablaNotasC.Item(NombreNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Nombre"))
                Me.TablaNotasC.Item(FantesNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("FechaAntesde"))
                Me.TablaNotasC.Item(FechaDNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("FechaDespuesde"))
                Try
                    If Trim(ds.Tables(0).Rows(i)("Clave")) <> "" Then
                        Fila.Cells(LetrasNotas.Index).Value = Me.LetrasNotas.Items(Obtener_indexLetra_Notas(Trim(ds.Tables(0).Rows(i)("Clave"))))

                    End If

                Catch ex As Exception

                End Try
                Me.TablaNotasC.Item(LetrasNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Clave"))
                Me.TablaNotasC.Item(EfeNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Efectivo"))
                Me.TablaNotasC.Item(TransfNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Transferencia"))
                Me.TablaNotasC.Item(ChequeNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Cheque"))

                Try
                    If Trim(ds.Tables(0).Rows(i)("Tipo_Poliza")) <> "" Then
                        Fila.Cells(TPOLNotas.Index).Value = Me.TPOLNotas.Items(Obtener_IndexTipoNotas(Trim(ds.Tables(0).Rows(i)("Tipo_Poliza"))))
                    End If

                Catch ex As Exception

                End Try
                Me.TablaNotasC.Item(PANotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("ProvisionAcred"))
                Me.TablaNotasC.Item(PPNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("ProvisionProveed"))
                posicion = posicion + 1
            Next
            Dim Emitidas As String = " SELECT DISTINCT Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor  FROM Xml_Sat WHERE Id_Empresa =" & Me.lstCliente.SelectItem & " and  tipo = 'NotaCredito' and Emitidas = 0 and Xml_Sat.RFC_Emisor not in (SELECT Notas_Pol_Modelo.RFC FROM     
                                                Notas_Pol_Modelo   WHERE    Emitidas =0  and Id_Empresa =" & Me.lstCliente.SelectItem & ") "
            Dim em As DataSet = Eventos.Obtener_DS(Emitidas)
            If em.Tables(0).Rows.Count > 0 Then
                Me.TablaNotasC.RowCount = Me.TablaNotasC.RowCount + em.Tables(0).Rows.Count
                For j As Integer = 0 To em.Tables(0).Rows.Count - 1
                    Me.TablaNotasC.Item(RFCNotas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("RFC_Emisor"))
                    Me.TablaNotasC.Item(NombreNotas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("Nombre_Emisor"))
                    posicion = posicion + 1
                Next
            End If

            'frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Else

            Dim Emitidas As String = " SELECT DISTINCT Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor  FROM Xml_Sat WHERE Id_Empresa =" & Me.lstCliente.SelectItem & " and tipo = 'NotaCredito' and Emitidas = 0 and Xml_Sat.RFC_Emisor not in (SELECT Notas_Pol_Modelo.RFC FROM     
                                                Notas_Pol_Modelo   WHERE     Emitidas = 0  and Id_Empresa =" & Me.lstCliente.SelectItem & ") "
            Dim em As DataSet = Eventos.Obtener_DS(Emitidas)
            If em.Tables(0).Rows.Count > 0 Then
                Me.TablaNotasC.RowCount = Me.TablaNotasC.RowCount + em.Tables(0).Rows.Count
                ContabilizacionNotas()

                For j As Integer = 0 To em.Tables(0).Rows.Count - 1
                    Me.TablaNotasC.Item(RFCNotas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("RFC_Emisor"))
                    Me.TablaNotasC.Item(NombreNotas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("Nombre_Emisor"))
                    posicion = posicion + 1
                Next
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If

        End If
    End Sub

    Private Sub BuscarC(ByVal Cliente As Integer)
        Dim Sql As String = "SELECT 	Id_Pol_Mod_Complemento,	RFC,	Nombre,	FechaAntesde,	FechaDespuesde,	Clave,	Efectivo,	Transferencia,	Banco_Origen,
	                            Banco_Destino,	Cheque,	Nom_Banco_Ch,	Tipo_Poliza,	Anticipo FROM dbo.Complementos_Pol_Modelo where Id_Empresa = " & Cliente & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaComplemento.RowCount = ds.Tables(0).Rows.Count
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaComplemento.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            'Cargar los bancos, Polizas y letras

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                ' Ver registros

                frm.Barra.Value = i
            Next
            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Else

        End If
    End Sub
    Private Function Obtener_indexB(ByVal valor As String)
        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BOFactura.Items.Count - 1
            If valor = Trim(Me.BOFactura.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice
    End Function
    Private Function Obtener_index2(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.TpolFactura.Items.Count - 1
            If valor = Trim(Me.TpolFactura.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_IndexTipoNotas(ByVal valor As String)
        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.TPOLNotas.Items.Count - 1
            If valor = Trim(Me.TPOLNotas.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice
    End Function
    Private Function Obtener_indexbN(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BOCHFactura.Items.Count - 1
            If valor = Trim(Me.BOCHFactura.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Sub Cargar_bancos(ByVal bancos As String)
        Dim sql As String = " SELECT rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias FROM Bancos_Clientes INNER JOIN Bancos ON Bancos_Clientes.Id_Banco =Bancos.Id_Banco  where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%" & bancos & "%'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If bancos = "Ch" Then
                'Cheque
                If Me.BOCHFactura.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BOCHFactura.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                Else
                    Try
                        Me.BOCHFactura.Items.Clear()
                    Catch ex As Exception

                    End Try

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BOCHFactura.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                End If
            Else
                'Transferencia
                'Origen
                If Me.BOFactura.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BOFactura.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                Else
                    Try
                        Me.BOFactura.Items.Clear()
                    Catch ex As Exception

                    End Try

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BOFactura.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                End If


            End If
        End If

    End Sub
    Private Sub Cargar_bancosDevC()

        '***************************    Modificar codigo para los nuevos bancos por rfc    **********************************
        Dim sql2 As String = " SELECT rtrim(Bancos.Clave) +'-'+ Bancos.Nombre AS Alias FROM Bancos "
        Dim ds2 As DataSet = Eventos.Obtener_DS(sql2)
        If ds2.Tables(0).Rows.Count > 0 Then
            If Me.BDComplementos.Items.Count = 0 Then
                For i As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                    Me.BDComplementos.Items.Add(ds2.Tables(0).Rows(i)("Alias"))
                Next
            Else
                Me.BDComplementos.Items.Clear()
                For i As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                    Me.BDComplementos.Items.Add(ds2.Tables(0).Rows(i)("Alias"))
                Next
            End If
        End If


    End Sub
    Private Sub Cargar_bancosC(ByVal bancos As String)
        Dim sql As String = " SELECT rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias FROM Bancos_Clientes INNER JOIN Bancos ON Bancos_Clientes.Id_Banco =Bancos.Id_Banco  where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%" & bancos & "%'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If bancos = "Ch" Then
                'Cheque
                If Me.BOCHComplementos.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BOCHComplementos.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                Else
                    Try
                        Me.BOCHComplementos.Items.Clear()
                    Catch ex As Exception

                    End Try

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BOCHComplementos.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                End If
            Else
                'Transferencia
                'Origen
                If Me.BOComplementos.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BOComplementos.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                Else
                    Try
                        Me.BOComplementos.Items.Clear()
                    Catch ex As Exception

                    End Try

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BOComplementos.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                End If


            End If
        End If

    End Sub
    Private Sub ContabilizacionFacturas()
        Dim contab As DataSet = Eventos.Obtener_DS(" Select Rtrim(Clave) as Clave from ClaveEgresos where ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & "  ")
        If contab.Tables(0).Rows.Count > 0 Then
            If Me.LetraFacturas.Items.Count = 0 Then

                For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                    Me.LetraFacturas.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                Next
            Else
                Me.LetraFacturas.Items.Clear()
                For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                    Me.LetraFacturas.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                Next
            End If
        End If
        Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
        If Tipo.Tables(0).Rows.Count > 0 Then
            If Me.TpolFactura.Items.Count = 0 Then
                For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                    Me.TpolFactura.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                Next
            Else
                Me.TpolFactura.Items.Clear()
                For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                    Me.TpolFactura.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                Next
            End If
        End If
    End Sub

    Private Sub CambiarBancoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CambiarBancoToolStripMenuItem.Click


        If Me.TablaFactura.Rows.Count > 0 Then

            'Verificar la columna del click secundario
            If Me.TablaFactura.CurrentCell.ColumnIndex = Me.BDFactura.Index Then 'Cuentas del Cargo

                Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Me.lstCliente.SelectItem & ") and RFC = '" & Me.TablaFactura.Item(RFCFacturas.Index, Me.TablaFactura.CurrentRow.Index).Value & "' order by Banco")
                Dim actividad(,) As String
                ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim largo As Integer = Len(ds.Tables(0).Rows(i)("Clabe"))
                    Dim cadena As String = Trim(ds.Tables(0).Rows(i)("Banco")) & " " & Trim(ds.Tables(0).Rows(i)("Clabe").ToString.Substring(largo - 3, 3))

                    actividad(0, i) = cadena
                    Debug.Print(Me.TablaFactura.Item(RFCFacturas.Index, Me.TablaFactura.CurrentRow.Index).Value)
                    actividad(1, i) = "0"
                Next
                With My.Forms.DialogUnaSeleccion
                    .limpiar()
                    .Titulo = Eventos.titulo_app
                    .Texto = "Selecciona el Banco"
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
                Me.TablaFactura.Item(BDFactura.Index, Me.TablaFactura.CurrentRow.Index).Value = descrip
                Me.TablaFactura_CellEndEdit(Me.TablaFactura, Nothing)
            End If
        Else

        End If



    End Sub

    Private Sub ContabilizacionComplementos()
        Dim contab As DataSet = Eventos.Obtener_DS(" Select 'C' as Clave union select 'GG' AS Clave  ")
        If contab.Tables(0).Rows.Count > 0 Then
            If Me.LetraComplementos.Items.Count = 0 Then

                For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                    Me.LetraComplementos.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                Next
            Else
                Me.LetraComplementos.Items.Clear()
                For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                    Me.LetraComplementos.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                Next
            End If
        End If
        Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
        If Tipo.Tables(0).Rows.Count > 0 Then
            If Me.TpolComplementos.Items.Count = 0 Then
                For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                    Me.TpolComplementos.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                Next
            Else
                Me.TpolComplementos.Items.Clear()
                For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                    Me.TpolComplementos.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                Next
            End If
        End If
    End Sub
    Private Sub ContabilizacionNotas()
        Dim contab As DataSet = Eventos.Obtener_DS(" Select 'C' as Clave union select 'GG' AS Clave  ")
        If contab.Tables(0).Rows.Count > 0 Then
            If Me.LetrasNotas.Items.Count = 0 Then

                For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                    Me.LetrasNotas.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                Next
            Else
                Me.LetrasNotas.Items.Clear()
                For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                    Me.LetrasNotas.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                Next
            End If
        End If
        Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
        If Tipo.Tables(0).Rows.Count > 0 Then
            If Me.TPOLNotas.Items.Count = 0 Then
                For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                    Me.TPOLNotas.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                Next
            Else
                Me.TPOLNotas.Items.Clear()
                For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                    Me.TPOLNotas.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                Next
            End If
        End If
    End Sub

    Private Sub TablaFactura_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaFactura.CellEndEdit
        If Me.TablaFactura.Item(TransfFactura.Index, Me.TablaFactura.CurrentRow.Index).Value = True Then
            If Me.TablaFactura.Item(BDFactura.Index, Me.TablaFactura.CurrentRow.Index).Value <> Nothing Then
            Else
                Me.TablaFactura.Item(BDFactura.Index, Me.TablaFactura.CurrentRow.Index).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaFactura.Item(RFCFacturas.Index, Me.TablaFactura.CurrentRow.Index).Value)
            End If

            If Trim(Me.TablaFactura.Item(TpolFactura.Index, Me.TablaFactura.CurrentRow.Index).Value) <> Nothing Then
                If Trim(Me.TablaFactura.Item(BOFactura.Index, Me.TablaFactura.CurrentRow.Index).Value) <> Nothing Then
                Else
                    Dim fila As DataGridViewRow = Me.TablaFactura.Rows(Me.TablaFactura.CurrentRow.Index)
                    Try
                        If Trim(Me.TablaFactura.Item(TpolFactura.Index, Me.TablaFactura.CurrentRow.Index).Value) <> "" Then
                            Dim largo As Integer = Len(Me.TablaFactura.Item(TpolFactura.Index, Me.TablaFactura.CurrentRow.Index).Value)
                            Dim posicion As Integer = InStr(1, Me.TablaFactura.Item(TpolFactura.Index, Me.TablaFactura.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaFactura.Item(TpolFactura.Index, Me.TablaFactura.CurrentRow.Index).Value.Substring(posicion + 1, largo - posicion - 1)
                            Dim indice As Integer = Obtener_indexBT(Al)
                            If indice = 1000 Then
                            Else
                                fila.Cells(BOFactura.Index).Value = Me.BOFactura.Items(indice)
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                End If

            End If
        Else
            Dim fila As DataGridViewRow = Me.TablaFactura.Rows(Me.TablaFactura.CurrentRow.Index)
            fila.Cells(BOFactura.Index).Value = ""
            Me.TablaFactura.Item(BDFactura.Index, Me.TablaFactura.CurrentRow.Index).Value = ""
        End If
        Candado(Me.TablaFactura.CurrentRow.Index)
    End Sub
    Private Function Banco_destino(ByVal Cliente As Integer, ByVal Rfc As String)
        Dim banco As String = ""
        Dim sql2 As String = " SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Cliente & ") and RFC = '" & Rfc & "' and Favorito=1"
        Dim ds2 As DataSet = Eventos.Obtener_DS(sql2)

        If ds2.Tables(0).Rows.Count > 0 Then
            Dim largo As Integer = Len(ds2.Tables(0).Rows(0)("Clabe"))
            Try
                banco = Trim(ds2.Tables(0).Rows(0)("Banco")) & " " & Trim(ds2.Tables(0).Rows(0)("Clabe").ToString.Substring(largo - 3, 3))
            Catch ex As Exception

            End Try
        Else
            banco = ""
        End If

        Return banco
    End Function
    Private Function Obtener_indexBT(ByVal valor As String)

        Dim Indice As Integer = 1000
        For i As Integer = 0 To Me.BOFactura.Items.Count - 1

            Dim largo As Integer = Len(Trim(Me.BOFactura.Items(i)))
            Dim posicion As Integer = InStr(1, Trim(Me.BOFactura.Items(i)), "-", CompareMethod.Binary)
            Dim Al As String = Trim(Me.BOFactura.Items(i)).Substring(posicion, largo - posicion)



            If Al = valor Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_index(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.LetraFacturas.Items.Count - 1
            If valor = Trim(Me.LetraFacturas.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexLetra_Notas(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.LetrasNotas.Items.Count - 1
            If valor = Trim(Me.LetrasNotas.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function

    Private Sub LstFacturas_TextChanged(sender As Object, e As EventArgs) Handles LstFacturas.TextChanged
        Dim posicion As Integer = 0
        If Me.lstCliente.SelectText <> "" Then
            If Me.lblfiltroFacturas.Text <> "" Then
                Me.TablaFactura.Rows.Clear()
                Dim Sql As String = " SELECT  Id_Pol_Mod_Factura,	RFC,	Nombre,	FechaAntesde,	FechaDespuesde,	Clave,	Efectivo,	Transferencia,	Banco_Origen,	Banco_Destino,	
                               Cheque,	Nom_Banco_Ch,	Tipo_Poliza,	Provision,	Anticipo FROM dbo.Facturas_Poliza_Modelo where Id_Empresa = " & Me.lstCliente.SelectItem & " and Facturas_Poliza_Modelo." & Me.lblfiltroFacturas.Text & " like '%" & Me.LstFacturas.Text & "%'"

                Dim ds As DataSet = Eventos.Obtener_DS(Sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.TablaFactura.RowCount = ds.Tables(0).Rows.Count

                    ' Dim contab As DataSet = Eventos.Obtener_DS(" Select Rtrim(Clave) as Clave from Letras_Contabilidad where Letras_Contabilidad.Id_Empresa = " & Me.lstCliente.SelectItem & "  ")
                    Dim contab As DataSet = Eventos.Obtener_DS(" Select Rtrim(Clave) as Clave from ClaveEgresos where ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & "  ")
                    If contab.Tables(0).Rows.Count > 0 Then
                        If Me.LetraFacturas.Items.Count = 0 Then

                            For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                                Me.LetraFacturas.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                            Next
                        Else
                            Me.LetraFacturas.Items.Clear()
                            For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                                Me.LetraFacturas.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                            Next
                        End If
                    End If
                    Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
                    If Tipo.Tables(0).Rows.Count > 0 Then
                        If Me.TpolFactura.Items.Count = 0 Then
                            For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                                Me.TpolFactura.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                            Next
                        Else
                            Me.TpolFactura.Items.Clear()
                            For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                                Me.TpolFactura.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                            Next
                        End If
                    End If

                    Cargar_bancos("Transf")
                    Cargar_bancos("Ch")

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim Fila As DataGridViewRow = Me.TablaFactura.Rows(i)
                        Me.TablaFactura.Item(IdFac.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Id_Pol_Mod_Factura"))
                        Me.TablaFactura.Item(RFCFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("RFC"))
                        Me.TablaFactura.Item(NomFactura.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Nombre"))
                        Me.TablaFactura.Item(FantesFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("FechaAntesde"))
                        Me.TablaFactura.Item(FDespuesFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("FechaDespuesde"))
                        Try
                            If Trim(ds.Tables(0).Rows(i)("Clave")) <> "" Then
                                Fila.Cells(LetraFacturas.Index).Value = Me.LetraFacturas.Items(Obtener_index(Trim(ds.Tables(0).Rows(i)("Clave"))))
                                '     Me.TablaImportar.Item(ContabilizacionC.INDEX, j).Value = Me.ContabilizacionC.Items(Obtener_index(ds.Tables(0).Rows(j)("Clave")))
                            End If

                        Catch ex As Exception

                        End Try
                        Me.TablaFactura.Item(LetraFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Clave"))
                        Me.TablaFactura.Item(EfecFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Efectivo"))
                        Me.TablaFactura.Item(TransfFactura.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Transferencia"))
                        Try
                            If Trim(ds.Tables(0).Rows(i)("Banco_Origen")) <> "" Then
                                Fila.Cells(BOFactura.Index).Value = Me.BOFactura.Items(Obtener_indexB(Trim(ds.Tables(0).Rows(i)("Banco_Origen"))))
                            End If
                        Catch ex As Exception

                        End Try

                        Me.TablaFactura.Item(BDFactura.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Banco_Destino"))
                        Me.TablaFactura.Item(ChequeFacura.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Cheque"))


                        Try
                            If Trim(ds.Tables(0).Rows(i)("Nom_Banco_Ch")) <> "" Then
                                Fila.Cells(BOCHFactura.Index).Value = Me.BOCHFactura.Items(Obtener_indexbN(Trim(ds.Tables(0).Rows(i)("Nom_Banco_Ch"))))
                            End If
                        Catch ex As Exception

                        End Try

                        Try
                            If Trim(ds.Tables(0).Rows(i)("Tipo_Poliza")) <> "" Then
                                Fila.Cells(TpolFactura.Index).Value = Me.TpolFactura.Items(Obtener_index2(Trim(ds.Tables(0).Rows(i)("Tipo_Poliza"))))
                            End If

                        Catch ex As Exception

                        End Try
                        Me.TablaFactura.Item(ProvFacturas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Provision"))
                        Me.TablaFactura.Item(AnticipoFactura.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Anticipo"))

                        posicion = posicion + 1

                    Next

                    Try
                        Dim Emitidas As String = " SELECT DISTINCT Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor  FROM Xml_Sat WHERE tipo = 'Factura' and Id_Empresa =" & Me.lstCliente.SelectItem & " and Emitidas = 0 and Xml_Sat.RFC_Emisor not in (SELECT Facturas_Poliza_Modelo.RFC FROM     
                                                Facturas_Poliza_Modelo   WHERE    Emitidas =0  and Id_Empresa =" & Me.lstCliente.SelectItem & " and Xml_Sat." & Me.lblfiltroFacturas.Text & "_Emisor like '%" & Me.LstFacturas.Text & "%') "
                        Dim em As DataSet = Eventos.Obtener_DS(Emitidas)
                        If em.Tables(0).Rows.Count > 0 Then
                            Me.TablaFactura.RowCount = Me.TablaFactura.RowCount + em.Tables(0).Rows.Count
                            For j As Integer = 0 To em.Tables(0).Rows.Count - 1
                                Me.TablaFactura.Item(RFCFacturas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("RFC_Emisor"))
                                Me.TablaFactura.Item(NomFactura.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("Nombre_Emisor"))
                                posicion = posicion + 1
                            Next
                        End If
                    Catch ex As Exception

                    End Try

                Else
                    Try
                        Dim Emitidas As String = " SELECT DISTINCT Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor  FROM Xml_Sat WHERE Id_Empresa =" & Me.lstCliente.SelectItem & " and  tipo = 'Factura' and Emitidas = 0 and Id_Empresa =" & Me.lstCliente.SelectItem & " and Xml_Sat." & Me.lblfiltroFacturas.Text & "_Emisor like '%" & Me.LstFacturas.Text & "%' "
                        Dim em As DataSet = Eventos.Obtener_DS(Emitidas)
                        If em.Tables(0).Rows.Count > 0 Then
                            Me.TablaFactura.RowCount = em.Tables(0).Rows.Count
                            ContabilizacionFacturas()
                            Cargar_bancos("Transf")
                            Cargar_bancos("Ch")
                            For j As Integer = 0 To em.Tables(0).Rows.Count - 1
                                Me.TablaFactura.Item(RFCFacturas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("RFC_Emisor"))
                                Me.TablaFactura.Item(NomFactura.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("Nombre_Emisor"))
                                posicion = posicion + 1
                            Next
                            ' MessageBox.Show("Proceso Terminado...", Eventos.Titulo_APP, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If
        End If
    End Sub



    Private Sub TablaFactura_Click(sender As Object, e As EventArgs) Handles TablaFactura.Click

        If TablaFactura.RowCount > 0 Then
            RaiseEvent Registro(TablaFactura.Item(0, TablaFactura.CurrentCell.RowIndex).Value.ToString)
            '  Me.LblFiltro.Text = "Bancos_RFC." & Me.TablaFactura.Columns(Me.TablaFactura.CurrentCell.ColumnIndex).HeaderText
            Me.lblfiltroFacturas.Text = Me.TablaFactura.Columns(Me.TablaFactura.CurrentCell.ColumnIndex).HeaderText
        End If
    End Sub

    Private Sub TablaNotasC_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaNotasC.CellEndEdit
        CandadoN(Me.TablaNotasC.CurrentRow.Index)
    End Sub



    Private Sub LstNotasCredito_TextChanged(sender As Object, e As EventArgs) Handles LstNotasCredito.TextChanged
        If Me.lstCliente.SelectText <> "" Then
            If Me.LstNotasCredito.Text <> "" Then
                Me.TablaNotasC.Rows.Clear()
                Dim posicion As Integer = 0
                Dim Sql As String = " SELECT 	Id_Pol_Mod_Nota,	RFC,	Nombre,	FechaAntesde,	FechaDespuesde,	Clave,	Efectivo,	
                                Transferencia,	Cheque,	Tipo_Poliza,	ProvisionAcred,	ProvisionProveed,	Id_Empresa FROM dbo.Notas_Pol_Modelo where Id_Empresa = " & Me.lstCliente.SelectItem & " and Notas_Pol_Modelo." & Me.lblNotasFiltro.Text & " like '%" & Me.LstNotasCredito.Text & "%' "

                Dim ds As DataSet = Eventos.Obtener_DS(Sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.TablaNotasC.RowCount = ds.Tables(0).Rows.Count

                    ContabilizacionNotas()

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim Fila As DataGridViewRow = Me.TablaFactura.Rows(i)
                        Me.TablaNotasC.Item(IDnOTA.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Id_Pol_Mod_Nota"))
                        Me.TablaNotasC.Item(RFCNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("RFC"))
                        Me.TablaNotasC.Item(NombreNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Nombre"))
                        Me.TablaNotasC.Item(FantesNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("FechaAntesde"))
                        Me.TablaNotasC.Item(FechaDNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("FechaDespuesde"))
                        Try
                            If Trim(ds.Tables(0).Rows(i)("Clave")) <> "" Then
                                Fila.Cells(LetrasNotas.Index).Value = Me.LetrasNotas.Items(Obtener_indexLetra_Notas(Trim(ds.Tables(0).Rows(i)("Clave"))))

                            End If

                        Catch ex As Exception

                        End Try
                        Me.TablaNotasC.Item(LetrasNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Clave"))
                        Me.TablaNotasC.Item(EfeNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Efectivo"))
                        Me.TablaNotasC.Item(TransfNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Transferencia"))
                        Me.TablaNotasC.Item(ChequeNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("Cheque"))

                        Try
                            If Trim(ds.Tables(0).Rows(i)("Tipo_Poliza")) <> "" Then
                                Fila.Cells(TPOLNotas.Index).Value = Me.TPOLNotas.Items(Obtener_IndexTipoNotas(Trim(ds.Tables(0).Rows(i)("Tipo_Poliza"))))
                            End If

                        Catch ex As Exception

                        End Try
                        Me.TablaNotasC.Item(PANotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("ProvisionAcred"))
                        Me.TablaNotasC.Item(PPNotas.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)("ProvisionProveed"))
                        posicion = posicion + 1
                    Next
                    Dim Emitidas As String = " SELECT DISTINCT Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor  FROM Xml_Sat WHERE Id_Empresa =" & Me.lstCliente.SelectItem & " and Emitidas = 0 and Xml_Sat.RFC_Emisor not in (SELECT Facturas_Poliza_Modelo.RFC FROM     
                                                Facturas_Poliza_Modelo   WHERE  tipo = 'NotaCredito' and Emitidas =0  and Id_Empresa =" & Me.lstCliente.SelectItem & " and Xml_Sat." & Me.lblNotasFiltro.Text & "_Emisor like '%" & Me.LstNotasCredito.Text & "%') "
                    Dim em As DataSet = Eventos.Obtener_DS(Emitidas)
                    If em.Tables(0).Rows.Count > 0 Then
                        Me.TablaNotasC.RowCount = Me.TablaNotasC.RowCount + em.Tables(0).Rows.Count
                        For j As Integer = 0 To em.Tables(0).Rows.Count - 1
                            Me.TablaNotasC.Item(RFCNotas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("RFC_Emisor"))
                            Me.TablaNotasC.Item(NombreNotas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("Nombre_Emisor"))
                            posicion = posicion + 1
                        Next
                    End If


                Else

                    Dim Emitidas As String = " SELECT DISTINCT Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor  FROM Xml_Sat WHERE  tipo = 'NotaCredito' and Emitidas = 0  and Id_Empresa =" & Me.lstCliente.SelectItem & " and Xml_Sat." & Me.lblNotasFiltro.Text & "_Emisor like '%" & Me.LstNotasCredito.Text & "%' "
                    Dim em As DataSet = Eventos.Obtener_DS(Emitidas)
                    If em.Tables(0).Rows.Count > 0 Then
                        Me.TablaNotasC.RowCount = em.Tables(0).Rows.Count
                        ContabilizacionNotas()

                        For j As Integer = 0 To em.Tables(0).Rows.Count - 1
                            Me.TablaNotasC.Item(RFCNotas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("RFC_Emisor"))
                            Me.TablaNotasC.Item(NombreNotas.Index, posicion).Value = Trim(em.Tables(0).Rows(j)("Nombre_Emisor"))
                            posicion = posicion + 1
                        Next

                    End If

                End If
            End If
        End If
    End Sub

    Private Sub TablaNotasC_Click(sender As Object, e As EventArgs) Handles TablaNotasC.Click

        If TablaNotasC.RowCount > 0 Then
            RaiseEvent Registro(TablaNotasC.Item(0, TablaNotasC.CurrentCell.RowIndex).Value.ToString)
            '  Me.LblFiltro.Text = "Bancos_RFC." & Me.TablaFactura.Columns(Me.TablaFactura.CurrentCell.ColumnIndex).HeaderText
            Me.lblNotasFiltro.Text = Me.TablaNotasC.Columns(Me.TablaNotasC.CurrentCell.ColumnIndex).HeaderText
        End If
    End Sub

    Private Sub TablaFactura_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaFactura.DataError

    End Sub


End Class