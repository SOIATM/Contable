Imports Telerik.WinControls
Public Class ClavesEgresos

    Dim Datos As DataSet
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub
    Private Sub CargarListas()

        Dim sql As String = " SELECT   Catalogo_de_Cuentas.Nivel1 +'-'+ Catalogo_de_Cuentas.Nivel2 +'-'+ Catalogo_de_Cuentas.Nivel3 +'-'+  Catalogo_de_Cuentas.Nivel4   + '/'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Id_Empresa = " & Me.lstCliente.SelectItem & " ORDER BY Alias"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.GravadoPUE.DataSource = ds.Tables(0)
            Me.GravadoPUE.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.ExentoPUE.DataSource = ds.Tables(0)
            Me.ExentoPUE.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.IVAPUE.DataSource = ds.Tables(0)
            Me.IVAPUE.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.RISRPUE.DataSource = ds.Tables(0)
            Me.RISRPUE.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.RIVAPUE.DataSource = ds.Tables(0)
            Me.RIVAPUE.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.OtraRPUE.DataSource = ds.Tables(0)
            Me.OtraRPUE.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.GravadoPPD.DataSource = ds.Tables(0)
            Me.GravadoPPD.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.ExentoPPD.DataSource = ds.Tables(0)
            Me.ExentoPPD.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.IVAPPD.DataSource = ds.Tables(0)
            Me.IVAPPD.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.RISRPPD.DataSource = ds.Tables(0)
            Me.RISRPPD.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.RIVAPPD.DataSource = ds.Tables(0)
            Me.RIVAPPD.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.OtraRPPD.DataSource = ds.Tables(0)
            Me.OtraRPPD.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString

            Me.AnticipoGPUE.DataSource = ds.Tables(0)
            Me.AnticipoGPUE.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.AnticipoEPUE.DataSource = ds.Tables(0)
            Me.AnticipoEPUE.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.IVAAPUE.DataSource = ds.Tables(0)
            Me.IVAAPUE.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.AnticipoGPPD.DataSource = ds.Tables(0)
            Me.AnticipoGPPD.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.AnticipoEPPD.DataSource = ds.Tables(0)
            Me.AnticipoEPPD.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.IVAAPPD.DataSource = ds.Tables(0)
            Me.IVAAPPD.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString

            'Nuevas
            Me.IVAPA.DataSource = ds.Tables(0)
            Me.IVAPA.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.Debe.DataSource = ds.Tables(0)
            Me.Debe.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString

            'Cuenta de Orden
            Me.COGC.DataSource = ds.Tables(0)
            Me.COGC.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.COGA.DataSource = ds.Tables(0)
            Me.COGA.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.COEC.DataSource = ds.Tables(0)
            Me.COEC.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.COEA.DataSource = ds.Tables(0)
            Me.COEA.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Me.DebeE.DataSource = ds.Tables(0)
            Me.DebeE.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Try

                sql = " SELECT 	 Clave FROM dbo.ClaveEgresos where Id_Empresa = " & Me.lstCliente.SelectItem & " union select '' as Clave "
                Dim ds2 = Eventos.Obtener_DS(sql)
                If ds2.Tables(0).Rows.Count > 0 Then
                    Me.Ligar.DataSource = ds2.Tables(0)
                    Me.Ligar.DisplayMember = ds2.Tables(0).Columns(0).Caption.ToString
                End If
            Catch ex As Exception

            End Try

        End If
    End Sub
    Private Sub Limpiar()
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.Clear()
        End If
    End Sub
    Private Sub Cargar()
        Try
            Datos.Clear()
            Limpiar()
        Catch ex As Exception

        End Try


        Dim posicion As Integer = 0
        Dim Sql As String = " SELECT 	Id_Clave,	Id_Empresa,Concepto,	Descripcion,	Clave,	Tasa,ligar ,	GravadoPUE, NivelG,	CargoG,	ExentoPUE,	"
        Sql &= " NivelE,	CargoE,	IVAPUE,	NivelI,	CargoI,	RISRPUE,	NivelRISR,	CargoRISR,	RIVAPUE,	NivelRIVA,	CargoRIVA,	"
        Sql &= " OtraRPUE,	NivelOtraR,	CargoOtraR,	GravadoPPD,	NivelGP,	CargoGP,	ExentoPPD,	NivelEP,	CargoEP, IVAPPD,"
        Sql &= " NivelIP,	CargoIP,	RISRPPD,	NivelRISRP,	CargoRISRP,	RIVAPPD,	NivelRIVAP,	CargoRIVAP,	OtraRPPD,	"
        Sql &= " NivelOtraRP,	CargoOtraRP, IVAPA, NivelIVAPA, CargoIVAPA,	Debe,	NivelD,	CargoD, "
        Sql &= " COGC,	NivelCOGC,	CargoCOGC,	COGA,	NivelCOGA,	CargoCOGA,	COEC,	NivelCOEC,	"
        Sql &= " CargoCOEC,		COEA,	NivelCOEA,	CargoCOEA,DebeE,NivelDE,CargoDE, Negativo,AnticipoGPUE,"
        Sql &= " NivelA,	CargoA,	AnticipoEPUE,	NivelAE,	CargoAE,	IVAAPUE,	NivelIA,	CargoIA,	AnticipoGPPD,"
        Sql &= " NivelAP,	CargoAP,	AnticipoEPPD,	NivelAEP,	CargoAEP,	IVAAPPD,	NivelIAP,	CargoIAP,"
        Sql &= " COGC,	NivelCOGC,	CargoCOGC,	COGA,	NivelCOGA,	CargoCOGA,	COEC,	NivelCOEC,	CargoCOEC,	COEA,"
        Sql &= " NivelCOEA,	CargoCOEA "
        Sql &= " FROM dbo.ClaveEgresos "
        Sql &= " where Id_Empresa = " & Me.lstCliente.SelectItem & ""

        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = ds.Tables(0).Rows.Count
            Me.LblFacturasPPD.Text = "Total de Claves Encontradas: " & Me.Tabla.RowCount
            Datos = ds
        Else
            Me.Tabla.Rows.Add()
        End If
    End Sub
    Private Sub Tabla_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles Tabla.EditingControlShowing

        Select Case Me.Tabla.CurrentCellAddress.X
            Case 8, 9, 12, 15, 18, 21, 34, 37, 40, 43, 24, 28, 31, 52, 46, 49, 56, 59, 62, 65, 68
                Try
                    Dim comboBox As DataGridViewComboBoxEditingControl = e.Control

                    comboBox.DropDownStyle = ComboBoxStyle.DropDown
                    comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                Catch ex As Exception

                End Try

        End Select

    End Sub
    Private Sub Calcular1(ByVal ds As DataSet)
        Dim frm As New BarraProcesovb
        frm.Show()
        Try
            frm.Barra.Minimum = 0
            frm.Text = "Cargando Claves por favor espere..."
            frm.Barra.Maximum = Me.Tabla.RowCount - 1
            CargarListas()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.Tabla.Rows(i)
                Try
                    Me.Tabla.Item(Id.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Id_Clave"))
                    Me.Tabla.Item(Nombre.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("Concepto")), "", ds.Tables(0).Rows(i)("Concepto"))
                    Me.Tabla.Item(Descripcion.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Descripcion"))
                    Me.Tabla.Item(Clave.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Clave"))
                    Me.Tabla.Item(Tasa.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Tasa"))
                    Me.Tabla.Item(Ligar.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("Ligar")), "", ds.Tables(0).Rows(i)("Ligar"))
                    Me.Tabla.Item(Negativo.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("Negativo")), 0, ds.Tables(0).Rows(i)("Negativo"))
                    Me.Tabla.Item(NivelG.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelG")), 0, ds.Tables(0).Rows(i)("NivelG"))
                    Me.Tabla.Item(CargoG.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoG")), 0, ds.Tables(0).Rows(i)("CargoG"))
                    Me.Tabla.Item(NivelE.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelE")), 0, ds.Tables(0).Rows(i)("NivelE"))
                    Me.Tabla.Item(CargoE.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoE")), 0, ds.Tables(0).Rows(i)("CargoE"))
                    Me.Tabla.Item(NivelI.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelI")), 0, ds.Tables(0).Rows(i)("NivelI"))
                    Me.Tabla.Item(CargoI.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoI")), 0, ds.Tables(0).Rows(i)("CargoI"))
                    Me.Tabla.Item(NivelRISR.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelRISR")), 0, ds.Tables(0).Rows(i)("NivelRISR"))
                    Me.Tabla.Item(CargoRISR.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoRISR")), 0, ds.Tables(0).Rows(i)("CargoRISR"))
                    Me.Tabla.Item(NivelRIVA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelRIVA")), 0, ds.Tables(0).Rows(i)("NivelRIVA"))
                    Me.Tabla.Item(CargoRIVA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoRIVA")), 0, ds.Tables(0).Rows(i)("CargoRIVA"))
                    Me.Tabla.Item(NivelOtraR.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelOtraR")), 0, ds.Tables(0).Rows(i)("NivelOtraR"))
                    Me.Tabla.Item(CargoOtraR.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoOtraR")), 0, ds.Tables(0).Rows(i)("CargoOtraR"))
                    Me.Tabla.Item(NivelGP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelGP")), 0, ds.Tables(0).Rows(i)("NivelGP"))
                    Me.Tabla.Item(CargoGP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoGP")), 0, ds.Tables(0).Rows(i)("CargoGP"))
                    Me.Tabla.Item(NivelEP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelEP")), 0, ds.Tables(0).Rows(i)("NivelEP"))
                    Me.Tabla.Item(CargoEP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoEP")), 0, ds.Tables(0).Rows(i)("CargoEP"))
                    Me.Tabla.Item(NivelIP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelIP")), 0, ds.Tables(0).Rows(i)("NivelIP"))
                    Me.Tabla.Item(CargoIP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoIP")), 0, ds.Tables(0).Rows(i)("CargoIP"))
                    Me.Tabla.Item(NivelRISRP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelRISRP")), 0, ds.Tables(0).Rows(i)("NivelRISRP"))
                    Me.Tabla.Item(CargoRISRP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoRISRP")), 0, ds.Tables(0).Rows(i)("CargoRISRP"))
                    Me.Tabla.Item(NivelRIVAP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelRIVAP")), 0, ds.Tables(0).Rows(i)("NivelRIVAP"))
                    Me.Tabla.Item(CargoRIVAP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoRIVAP")), 0, ds.Tables(0).Rows(i)("CargoRIVAP"))
                    Me.Tabla.Item(NivelOtraRP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelOtraRP")), 0, ds.Tables(0).Rows(i)("NivelOtraRP"))
                    Me.Tabla.Item(CargoOtraRP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoOtraRP")), 0, ds.Tables(0).Rows(i)("CargoOtraRP"))
                    Me.Tabla.Item(NivelD.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelD")), 0, ds.Tables(0).Rows(i)("NivelD"))
                    Me.Tabla.Item(CargoD.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoD")), 0, ds.Tables(0).Rows(i)("CargoD"))
                    Me.Tabla.Item(NivelCOGC.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelCOGC")), 0, ds.Tables(0).Rows(i)("NivelCOGC"))
                    Me.Tabla.Item(CargoCOGC.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoCOGC")), 0, ds.Tables(0).Rows(i)("CargoCOGC"))
                    Me.Tabla.Item(NivelCOGA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelCOGA")), 0, ds.Tables(0).Rows(i)("NivelCOGA"))
                    Me.Tabla.Item(CargoCOGA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoCOGA")), 0, ds.Tables(0).Rows(i)("CargoCOGA"))
                    Me.Tabla.Item(NivelCOEC.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelCOEC")), 0, ds.Tables(0).Rows(i)("NivelCOEC"))
                    Me.Tabla.Item(CargoCOEC.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoCOEC")), 0, ds.Tables(0).Rows(i)("CargoCOEC"))
                    Me.Tabla.Item(NivelCOEA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelCOEA")), 0, ds.Tables(0).Rows(i)("NivelCOEA"))
                    Me.Tabla.Item(CargoCOEA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoCOEA")), 0, ds.Tables(0).Rows(i)("CargoCOEA"))
                    Me.Tabla.Item(NivelDE.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelDE")), 0, ds.Tables(0).Rows(i)("NivelDE"))
                    Me.Tabla.Item(CargoDE.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoDE")), 0, ds.Tables(0).Rows(i)("CargoDE"))

                    Me.Tabla.Item(NivelA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelA")), 0, ds.Tables(0).Rows(i)("NivelA"))
                    Me.Tabla.Item(CargoA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoA")), 0, ds.Tables(0).Rows(i)("CargoA"))
                    Me.Tabla.Item(NivelAE.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelAE")), 0, ds.Tables(0).Rows(i)("NivelAE"))
                    Me.Tabla.Item(CargoAE.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoAE")), 0, ds.Tables(0).Rows(i)("CargoAE"))

                    Me.Tabla.Item(NivelIA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelIA")), 0, ds.Tables(0).Rows(i)("NivelIA"))
                    Me.Tabla.Item(CargoIA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoIA")), 0, ds.Tables(0).Rows(i)("CargoIA"))

                    Me.Tabla.Item(NivelIA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelIA")), 0, ds.Tables(0).Rows(i)("NivelIA"))
                    Me.Tabla.Item(CargoIA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoIA")), 0, ds.Tables(0).Rows(i)("CargoIA"))

                    Me.Tabla.Item(NivelAP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelAP")), 0, ds.Tables(0).Rows(i)("NivelAP"))
                    Me.Tabla.Item(CargoAP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoAP")), 0, ds.Tables(0).Rows(i)("CargoAP"))

                    Me.Tabla.Item(NivelAEP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelAEP")), 0, ds.Tables(0).Rows(i)("NivelAEP"))
                    Me.Tabla.Item(CargoAEP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoAEP")), 0, ds.Tables(0).Rows(i)("CargoAEP"))

                    Me.Tabla.Item(NivelAEP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelAEP")), 0, ds.Tables(0).Rows(i)("NivelAEP"))
                    Me.Tabla.Item(CargoAEP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoAEP")), 0, ds.Tables(0).Rows(i)("CargoAEP"))

                    Me.Tabla.Item(NivelIAP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelIAP")), 0, ds.Tables(0).Rows(i)("NivelIAP"))
                    Me.Tabla.Item(CargoIAP.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoIAP")), 0, ds.Tables(0).Rows(i)("CargoIAP"))

                    Me.Tabla.Item(NivelCOGC.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelCOGC")), 0, ds.Tables(0).Rows(i)("NivelCOGC"))
                    Me.Tabla.Item(CargoCOGC.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoCOGC")), 0, ds.Tables(0).Rows(i)("CargoCOGC"))

                    Me.Tabla.Item(NivelCOGA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelCOGA")), 0, ds.Tables(0).Rows(i)("NivelCOGA"))
                    Me.Tabla.Item(CargoCOGA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoCOGA")), 0, ds.Tables(0).Rows(i)("CargoCOGA"))

                    Me.Tabla.Item(NivelCOEC.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelCOEC")), 0, ds.Tables(0).Rows(i)("NivelCOEC"))
                    Me.Tabla.Item(CargoCOEC.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoCOEC")), 0, ds.Tables(0).Rows(i)("CargoCOEC"))

                    Me.Tabla.Item(NivelCOEA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("NivelCOEA")), 0, ds.Tables(0).Rows(i)("NivelCOEA"))
                    Me.Tabla.Item(CargoCOEA.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("CargoCOEA")), 0, ds.Tables(0).Rows(i)("CargoCOEA"))


                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("COEA")) <> "" Then
                        Fila.Cells(COEA.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("COEA")), Me.COEA)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("COEC")) <> "" Then
                        Fila.Cells(COEC.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("COEC")), Me.COEC)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("COGA")) <> "" Then
                        Fila.Cells(COGA.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("COGA")), Me.COGA)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If Trim(ds.Tables(0).Rows(i)("COGC")) <> "" Then
                        Fila.Cells(COGC.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("COGC")), Me.COGC)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If Trim(ds.Tables(0).Rows(i)("IVAAPPD")) <> "" Then
                        Fila.Cells(IVAAPPD.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("IVAAPPD")), Me.IVAAPPD)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If Trim(ds.Tables(0).Rows(i)("AnticipoEPPD")) <> "" Then
                        Fila.Cells(AnticipoEPPD.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("AnticipoEPPD")), Me.AnticipoEPPD)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If Trim(ds.Tables(0).Rows(i)("AnticipoGPPD")) <> "" Then
                        Fila.Cells(AnticipoGPPD.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("AnticipoGPPD")), Me.AnticipoGPPD)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IVAAPUE")) <> "" Then
                        Fila.Cells(IVAAPUE.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("IVAAPUE")), Me.IVAAPUE)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If Trim(ds.Tables(0).Rows(i)("AnticipoEPUE")) <> "" Then
                        Fila.Cells(AnticipoEPUE.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("AnticipoEPUE")), Me.AnticipoEPUE)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("AnticipoGPUE")) <> "" Then
                        Fila.Cells(AnticipoGPUE.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("AnticipoGPUE")), Me.AnticipoGPUE)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("DebeE")) <> "" Then
                        Fila.Cells(DebeE.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("DebeE")), Me.DebeE)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("COGC")) <> "" Then
                        Fila.Cells(COGC.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("COGC")), Me.COGC)
                    End If
                Catch ex As Exception
                End Try


                Try
                    If Trim(ds.Tables(0).Rows(i)("COGA")) <> "" Then
                        Fila.Cells(COGA.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("COGA")), Me.COGA)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If Trim(ds.Tables(0).Rows(i)("COEC")) <> "" Then
                        Fila.Cells(COEC.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("COEC")), Me.COEC)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("COEA")) <> "" Then
                        Fila.Cells(COEA.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("COEA")), Me.COEA)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If Trim(ds.Tables(0).Rows(i)("Debe")) <> "" Then
                        Fila.Cells(Debe.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("Debe")), Me.Debe)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IVAPA")) <> "" Then
                        Fila.Cells(IVAPA.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("IVAPA")), Me.IVAPA)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("GravadoPUE")) <> "" Then
                        Fila.Cells(GravadoPUE.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("GravadoPUE")), Me.GravadoPUE)
                        'Me.GravadoPUE.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("GravadoPUE")), Me.GravadoPUE))
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("GravadoPPD")) <> "" Then
                        Fila.Cells(GravadoPPD.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("GravadoPPD")), Me.GravadoPPD)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("ExentoPUE")) <> "" Then
                        Fila.Cells(ExentoPUE.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("ExentoPUE")), Me.ExentoPUE)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IVAPUE")) <> "" Then
                        Fila.Cells(IVAPUE.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("IVAPUE")), Me.IVAPUE)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("RISRPUE")) <> "" Then
                        Fila.Cells(RISRPUE.Index).Value = Me.RISRPUE.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("RISRPUE")), Me.RISRPUE))
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("RIVAPUE")) <> "" Then
                        Fila.Cells(RIVAPUE.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("RIVAPUE")), Me.RIVAPUE)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("OtraRPUE")) <> "" Then
                        Fila.Cells(OtraRPUE.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("OtraRPUE")), Me.OtraRPUE)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("ExentoPPD")) <> "" Then
                        Fila.Cells(ExentoPPD.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("ExentoPPD")), Me.ExentoPPD)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IVAPPD")) <> "" Then
                        Fila.Cells(IVAPPD.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("IVAPPD")), Me.IVAPPD)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("RISRPPD")) <> "" Then
                        Fila.Cells(RISRPPD.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("RISRPPD")), Me.RISRPPD)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("RIVAPPD")) <> "" Then
                        Fila.Cells(RIVAPPD.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("RIVAPPD")), Me.RIVAPPD)
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("OtraRPPD")) <> "" Then
                        Fila.Cells(OtraRPPD.Index).Value = Obtener_Index(Trim(ds.Tables(0).Rows(i)("OtraRPPD")), Me.OtraRPPD)
                    End If
                Catch ex As Exception
                End Try
                frm.Barra.value = i
            Next

            frm.Close()
        Catch ex As Exception
            frm.Close()
        End Try
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

    End Sub
    Private Sub TablaVentas_EditingControlShowing(ByVal sender As Object, ByVal e As DataGridViewEditingControlShowingEventArgs)
        Dim ctrl As Control = DirectCast(e.Control, Control)
        If (TypeOf ctrl Is DataGridViewComboBoxEditingControl) Then
            Dim cb As ComboBox = DirectCast(ctrl, ComboBox)
            cb.DropDownStyle = ComboBoxStyle.DropDown
        End If
    End Sub
    Private Sub CmdBuscarF_Click(sender As Object, e As EventArgs) Handles CmdBuscarF.Click
        Me.Tabla.Enabled = False
        If Me.Tabla.RowCount > 0 Then
            Me.Tabla.Rows.Clear()
        End If
        Cargar()
        SP.RunWorkerAsync(Me.Tabla)
        Control.CheckForIllegalCrossThreadCalls = False
        Me.Tabla.Enabled = True
    End Sub

    Private Sub ClavezEgresos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTabla(Me.Tabla)

        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
        Me.CmdBuscarF.PerformClick()
    End Sub

    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        Me.Tabla.Rows.Add()
    End Sub

    Private Sub Tabla_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles Tabla.DataError

    End Sub

    Private Function Obtener_Index(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer = -1
        Dim ds As DataSet = Eventos.Obtener_DS(" select Catalogo_de_Cuentas.Nivel1 +'-'+ Catalogo_de_Cuentas.Nivel2 +'-'+ Catalogo_de_Cuentas.Nivel3 +'-'+  Catalogo_de_Cuentas.Nivel4   + '/'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE  cuenta= " & Trim(valor) & "  and Id_EMPRESA = " & Me.lstCliente.SelectItem & " ")
        valor = ds.Tables(0).Rows(0)(0)
        'For i As Integer = 0 To Col.Items.Count - 1
        '    If valor = Trim(Col.Items(i)) Then
        '        Indice = i
        '        Exit For
        '    End If
        'Next

        Return valor
    End Function
    Private Function Cuenta(ByVal Leyenda As String)
        Dim cta As String = ""
        If Leyenda = Nothing Then
            cta = ""
        Else
            Dim posi As Integer = InStr(1, Trim(Leyenda), "/", CompareMethod.Binary)
            Dim cuantos As Integer = Len(Trim(Leyenda)) - Len(Trim(Leyenda).Substring(0, posi))
            cta = Trim(Leyenda).Substring(0, 16)
        End If
        Return cta
    End Function

    Private Sub SP_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        Try
            Calcular1(Datos)
            Datos.Clear()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Tabla_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Tabla.CellPainting
        Try
            If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
                If Me.Tabla.Columns(e.ColumnIndex).Name = "Eliminar" Then
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All)
                    Dim icoAtomico As Icon = Global.ATMFiscal.My.Resources.Resources.Eliminar16
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 23, e.CellBounds.Top + 5)
                    Me.Tabla.Columns(Eliminar.Index).DefaultCellStyle.BackColor = Color.Blue
                    Me.Tabla.Rows(e.RowIndex).Height = icoAtomico.Height + 10
                    Me.Tabla.Columns(Eliminar.Index).Width = icoAtomico.Width + 50
                    e.Handled = True
                End If
            End If
            If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
                If Me.Tabla.Columns(e.ColumnIndex).Name = "Guardar" Then
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All)
                    Dim icoAtomico As Icon = Global.ATMFiscal.My.Resources.Resources.Guardar_16
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 23, e.CellBounds.Top + 5)
                    Me.Tabla.Columns(Guardar.Index).DefaultCellStyle.BackColor = Color.Blue
                    Me.Tabla.Rows(e.RowIndex).Height = icoAtomico.Height + 10
                    Me.Tabla.Columns(Guardar.Index).Width = icoAtomico.Width + 50
                    e.Handled = True
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Tabla_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Tabla.CellClick

        If e.ColumnIndex = Me.Tabla.Columns(Eliminar.Index).Index Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Me.Tabla.Item(Id.Index, e.RowIndex).Value <> Nothing Then
                    If Eventos.Comando_sql("Delete from dbo.ClaveEgresos where Id_Clave=" & Me.Tabla.Item(Id.Index, e.RowIndex).Value) > 0 Then
                        RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                    Else
                        RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                    End If
                End If
            End If
            Me.CmdBuscarF.PerformClick()
        ElseIf e.ColumnIndex = Me.Tabla.Columns(Guardar.Index).Index Then
            If Me.Tabla.Item(Id.Index, e.RowIndex).Value <> Nothing Then
                Editar(Me.Tabla.Item(Id.Index, e.RowIndex).Value, Me.lstCliente.SelectItem, Me.Tabla.Item(Ligar.Index, e.RowIndex).Value, Me.Tabla.Item(Nombre.Index, e.RowIndex).Value, Me.Tabla.Item(Descripcion.Index, e.RowIndex).Value, Me.Tabla.Item(Clave.Index, e.RowIndex).Value, Me.Tabla.Item(Tasa.Index, e.RowIndex).Value,
 IIf(Me.Tabla.Item(GravadoPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(GravadoPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelG.Index, e.RowIndex).Value, Me.Tabla.Item(CargoG.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(ExentoPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(ExentoPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelE.Index, e.RowIndex).Value, Me.Tabla.Item(CargoE.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(IVAPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(IVAPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelI.Index, e.RowIndex).Value, Me.Tabla.Item(CargoI.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(RISRPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(RISRPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelRISR.Index, e.RowIndex).Value, Me.Tabla.Item(CargoRISR.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(RIVAPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(RIVAPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelRIVA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoRIVA.Index, e.RowIndex).Value,
 IIf(Me.Tabla.Item(OtraRPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(OtraRPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelOtraR.Index, e.RowIndex).Value, Me.Tabla.Item(CargoOtraR.Index, e.RowIndex).Value,
 IIf(Me.Tabla.Item(GravadoPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(GravadoPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelGP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoGP.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(ExentoPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(ExentoPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelEP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoEP.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(IVAPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(IVAPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelIP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoIP.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(RISRPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(RISRPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelRISRP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoRISRP.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(RIVAPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(RIVAPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelRIVAP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoRIVAP.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(OtraRPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(OtraRPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelOtraRP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoOtraRP.Index, e.RowIndex).Value,
Me.Tabla.Item(Negativo.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(AnticipoGPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(AnticipoGPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoA.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(AnticipoEPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(AnticipoEPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelAE.Index, e.RowIndex).Value, Me.Tabla.Item(CargoAE.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(IVAAPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(IVAAPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelIA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoIA.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(AnticipoGPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(AnticipoGPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelAP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoAP.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(AnticipoEPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(AnticipoEPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelAEP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoAEP.Index, e.RowIndex).Value,
    IIf(Me.Tabla.Item(IVAAPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(IVAAPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelIAP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoIAP.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(IVAPA.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(IVAPA.Index, e.RowIndex).Value), Me.Tabla.Item(NivelIVAPA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoIVAPA.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(Debe.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(Debe.Index, e.RowIndex).Value), Me.Tabla.Item(NivelD.Index, e.RowIndex).Value, Me.Tabla.Item(CargoD.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(COGC.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(COGC.Index, e.RowIndex).Value), Me.Tabla.Item(NivelCOGC.Index, e.RowIndex).Value, Me.Tabla.Item(CargoCOGC.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(COGA.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(COGA.Index, e.RowIndex).Value), Me.Tabla.Item(NivelCOGA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoCOGA.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(COEC.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(COEC.Index, e.RowIndex).Value), Me.Tabla.Item(NivelCOEC.Index, e.RowIndex).Value, Me.Tabla.Item(CargoCOEC.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(COEA.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(COEA.Index, e.RowIndex).Value), Me.Tabla.Item(NivelCOEA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoCOEA.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(DebeE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(DebeE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelDE.Index, e.RowIndex).Value, Me.Tabla.Item(CargoDE.Index, e.RowIndex).Value)
            Else
                Insertar(Me.lstCliente.SelectItem, Me.Tabla.Item(Ligar.Index, e.RowIndex).Value, Me.Tabla.Item(Nombre.Index, e.RowIndex).Value, Me.Tabla.Item(Descripcion.Index, e.RowIndex).Value, Me.Tabla.Item(Clave.Index, e.RowIndex).Value, Me.Tabla.Item(Tasa.Index, e.RowIndex).Value,
 IIf(Me.Tabla.Item(GravadoPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(GravadoPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelG.Index, e.RowIndex).Value, Me.Tabla.Item(CargoG.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(ExentoPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(ExentoPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelE.Index, e.RowIndex).Value, Me.Tabla.Item(CargoE.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(IVAPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(IVAPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelI.Index, e.RowIndex).Value, Me.Tabla.Item(CargoI.Index, e.RowIndex).Value,
IIf(Me.Tabla.Item(RISRPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(RISRPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelRISR.Index, e.RowIndex).Value, Me.Tabla.Item(CargoRISR.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(RIVAPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(RIVAPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelRIVA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoRIVA.Index, e.RowIndex).Value,
 IIf(Me.Tabla.Item(OtraRPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(OtraRPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelOtraR.Index, e.RowIndex).Value, Me.Tabla.Item(CargoOtraR.Index, e.RowIndex).Value,
 IIf(Me.Tabla.Item(GravadoPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(GravadoPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelGP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoGP.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(ExentoPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(ExentoPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelEP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoEP.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(IVAPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(IVAPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelIP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoIP.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(RISRPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(RISRPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelRISRP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoRISRP.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(RIVAPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(RIVAPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelRIVAP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoRIVAP.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(OtraRPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(OtraRPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelOtraRP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoOtraRP.Index, e.RowIndex).Value,
Me.Tabla.Item(Negativo.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(AnticipoGPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(AnticipoGPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoA.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(AnticipoEPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(AnticipoEPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelAE.Index, e.RowIndex).Value, Me.Tabla.Item(CargoAE.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(IVAAPUE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(IVAAPUE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelIA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoIA.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(AnticipoGPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(AnticipoGPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelAP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoAP.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(AnticipoEPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(AnticipoEPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelAEP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoAEP.Index, e.RowIndex).Value,
    IIf(Me.Tabla.Item(IVAAPPD.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(IVAAPPD.Index, e.RowIndex).Value), Me.Tabla.Item(NivelIAP.Index, e.RowIndex).Value, Me.Tabla.Item(CargoIAP.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(IVAPA.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(IVAPA.Index, e.RowIndex).Value), Me.Tabla.Item(NivelIVAPA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoIVAPA.Index, e.RowIndex).Value,
   IIf(Me.Tabla.Item(Debe.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(Debe.Index, e.RowIndex).Value), Me.Tabla.Item(NivelD.Index, e.RowIndex).Value, Me.Tabla.Item(CargoD.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(COGC.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(COGC.Index, e.RowIndex).Value), Me.Tabla.Item(NivelCOGC.Index, e.RowIndex).Value, Me.Tabla.Item(CargoCOGC.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(COGA.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(COGA.Index, e.RowIndex).Value), Me.Tabla.Item(NivelCOGA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoCOGA.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(COEC.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(COEC.Index, e.RowIndex).Value), Me.Tabla.Item(NivelCOEC.Index, e.RowIndex).Value, Me.Tabla.Item(CargoCOEC.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(COEA.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(COEA.Index, e.RowIndex).Value), Me.Tabla.Item(NivelCOEA.Index, e.RowIndex).Value, Me.Tabla.Item(CargoCOEA.Index, e.RowIndex).Value,
  IIf(Me.Tabla.Item(DebeE.Index, e.RowIndex).Value Is Nothing, "", Me.Tabla.Item(DebeE.Index, e.RowIndex).Value), Me.Tabla.Item(NivelDE.Index, e.RowIndex).Value, Me.Tabla.Item(CargoDE.Index, e.RowIndex).Value)
            End If

        End If
    End Sub

    Private Sub CmdExportarf_Click(sender As Object, e As EventArgs) Handles CmdExportarf.Click
        Dim Dato As Object
        If Me.Tabla.RowCount > 0 Then
            If Me.Tabla.Columns.Count > 256 Then
                RadMessageBox.Show("El rango de fechas sobrepasa las columnas de una hoja de excel, disminuye el rango...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

                Exit Sub
            End If
            Dim excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcel("vacio", False)
            For col As Integer = 1 To Me.Tabla.Columns.Count - 1
                Eventos.EscribeExcel(excel, 1, col, Me.Tabla.Columns(col).HeaderText)
            Next
            For i As Integer = 0 To Me.Tabla.RowCount - 1
                For j As Integer = 1 To Me.Tabla.Columns.Count - 1
                    If j = 1 Then
                        If Me.Tabla.Item(j, i).Value.ToString <> "" Then
                            Dato = Valor(Me.Tabla.Item(j, i).Value.ToString.Substring(0, 4) & "-" & Me.Tabla.Item(j, i).Value.ToString.Substring(4, 4) & "-" & Me.Tabla.Item(j, i).Value.ToString.Substring(8, 4) & "-" & Me.Tabla.Item(j, i).Value.ToString.Substring(12, 4))
                        End If
                    Else
                        Dato = Valor(Me.Tabla.Item(j, i).Value)
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 1, "Claves Egresos")
                Next
            Next
            Eventos.Mostrar_Excel(excel)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay datos para exportar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

        End If
    End Sub
    Private Sub Insertar(ByVal Id_Cliente As Integer, ByVal li As String, ByVal Concepto As String, ByVal Descripcion1 As String, ByVal Clave1 As String, ByVal Tasa1 As Decimal, ByVal GravadoPUE1 As String, ByVal NivelG1 As String, ByVal CargoG1 As Boolean, ByVal ExentoPUE1 As String,
        ByVal NivelE1 As String, ByVal CargoE1 As Boolean, ByVal IVAPUE1 As String, ByVal NivelI1 As String, ByVal CargoI1 As Boolean, ByVal RISRPUE1 As String, ByVal NivelRISR1 As String, ByVal CargoRISR1 As Boolean,
                         ByVal RIVAPUE1 As String, ByVal NivelRIVA1 As String, ByVal CargoRIVA1 As Boolean, ByVal OtraRPUE1 As String, ByVal NivelOtraR1 As String, ByVal CargoOtraR1 As Boolean, ByVal GravadoPPD1 As String, ByVal NivelGP1 As String, ByVal CargoGP1 As Boolean, ByVal ExentoPPD1 As String,
         ByVal NivelEP1 As String, ByVal CargoEP1 As Boolean, ByVal IVAPPD1 As String, ByVal NivelIP1 As String, ByVal CargoIP1 As Boolean, ByVal RISRPPD1 As String, ByVal NivelRISRP1 As String, ByVal CargoRISRP1 As Boolean, ByVal RIVAPPD1 As String,
         ByVal NivelRIVAP1 As String, ByVal CargoRIVAP1 As Boolean, ByVal OtraRPPD1 As String, ByVal NivelOtraRP1 As String, ByVal CargoOtraRP1 As Boolean, ByVal Negativo1 As Boolean, ByVal AnticipoGPUE1 As String,
         ByVal NivelA1 As String, ByVal CargoA1 As Boolean, ByVal AnticipoEPUE1 As String, ByVal NivelAE1 As String, ByVal CargoAE1 As Boolean, ByVal IVAAPUE1 As String, ByVal NivelIA1 As String, ByVal CargoIA1 As Boolean, ByVal AnticipoGPPD1 As String,
         ByVal NivelAP1 As String, ByVal CargoAP1 As Boolean, ByVal AnticipoEPPD1 As String, ByVal NivelAEP1 As String, ByVal CargoAEP1 As Boolean, ByVal IVAAPPD1 As String, ByVal NivelIAP1 As String, ByVal CargoIAP1 As Boolean,
         ByVal IVAPA1 As String, ByVal NivelIVAPA1 As String, ByVal CargoIVAPA1 As Boolean, ByVal Debe1 As String, ByVal NivelD1 As String, ByVal CargoD1 As Boolean,
         ByVal COGC1 As String, ByVal NivelCOGC1 As String, ByVal CargoCOGC1 As Boolean, ByVal COGA1 As String, ByVal NivelCOGA1 As String, ByVal CargoCOGA1 As Boolean, ByVal COEC1 As String, ByVal NivelCOEC1 As String, ByVal CargoCOEC1 As Boolean,
         ByVal COEA1 As String, ByVal NivelCOEA1 As String, ByVal CargoCOEA1 As Boolean, ByVal DebeE1 As String, ByVal NivelDE1 As String, ByVal CargoDE1 As Boolean)
        Concepto = Limitar(Concepto)
        Dim Sql = "        INSERT INTO dbo.ClaveEgresos "
        Sql &= "( "
        Sql &= "	ID_empresa,Ligar, Concepto,  Descripcion,    Clave,  Tasa,   GravadoPUE,    NivelG,   CargoG,  ExentoPUE ,"
        Sql &= "    NivelE,  CargoE,   IVAPUE, NivelI,  CargoI,  RISRPUE, NivelRISR, CargoRISR,   RIVAPUE,   NivelRIVA, "
        Sql &= "    CargoRIVA,   OtraRPUE,   NivelOtraR, CargoOtraR,   GravadoPPD,  NivelGP,  CargoGP,    ExentoPPD, "
        Sql &= "    NivelEP,   CargoEP,   IVAPPD,   NivelIP,   CargoIP,   RISRPPD,   NivelRISRP,  CargoRISRP,  RIVAPPD, "
        Sql &= "    NivelRIVAP   ,   CargoRIVAP,  OtraRPPD,  NivelOtraRP,   CargoOtraRP,    Negativo,   AnticipoGPUE, "
        Sql &= "    NivelA,  CargoA,  AnticipoEPUE,   NivelAE,   CargoAE, IVAAPUE,  NivelIA,    CargoIA,   AnticipoGPPD, "
        Sql &= "    NivelAP,  CargoAP,   AnticipoEPPD,   NivelAEP, CargoAEP,   IVAAPPD,   NivelIAP,  CargoIAP, IVAPA,	NivelIVAPA,	CargoIVAPA,Debe,	NivelD,	CargoD, "
        Sql &= "	COGC,	NivelCOGC,	CargoCOGC,	COGA,	NivelCOGA,	CargoCOGA,	COEC,	NivelCOEC,	CargoCOEC,	COEA,	NivelCOEA,	CargoCOEA,DebeE ,NivelDE ,CargoDE ) "
        Sql &= "VALUES "
        Sql &= "	( "

        Sql &= " " & Id_Cliente & "	,"
        Sql &= "	'" & li & "', "
        Sql &= "	'" & Concepto & "', "
        Sql &= "	'" & Descripcion1 & "', "
        Sql &= "	'" & Clave1 & "', "
        Sql &= "	" & Tasa1 & ", "
        Sql &= "	'" & Regresacuenta(GravadoPUE1) & "', "
        Sql &= "	" & RegresaNivel(NivelG1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoG1) & ", "
        Sql &= "	'" & Regresacuenta(ExentoPUE1) & "', "
        Sql &= "	" & RegresaNivel(NivelE1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoE1) & ", "
        Sql &= "	'" & Regresacuenta(IVAPUE1) & "', "
        Sql &= "	" & RegresaNivel(NivelI1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoI1) & ", "
        Sql &= "	'" & Regresacuenta(RISRPUE1) & "', "
        Sql &= "	" & RegresaNivel(NivelRISR1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoRISR1) & ", "
        Sql &= "	'" & Regresacuenta(RIVAPUE1) & "', "
        Sql &= "	" & RegresaNivel(NivelRIVA1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoRIVA1) & ", "
        Sql &= "	'" & Regresacuenta(OtraRPUE1) & "', "
        Sql &= "	" & RegresaNivel(NivelOtraR1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoOtraR1) & ", "
        Sql &= "	'" & Regresacuenta(GravadoPPD1) & "', "
        Sql &= "	" & RegresaNivel(NivelGP1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoGP1) & ", "
        Sql &= "	'" & Regresacuenta(ExentoPPD1) & "', "
        Sql &= "	" & RegresaNivel(NivelEP1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoEP1) & ", "
        Sql &= "	'" & Regresacuenta(IVAPPD1) & "', "
        Sql &= "	" & RegresaNivel(NivelIP1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoIP1) & ", "
        Sql &= "	'" & Regresacuenta(RISRPPD1) & "', "
        Sql &= "	" & RegresaNivel(NivelRISRP1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoRISRP1) & ", "
        Sql &= "	'" & Regresacuenta(RIVAPPD1) & "', "
        Sql &= "	" & RegresaNivel(NivelRIVAP1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoRIVAP1) & ", "
        Sql &= "	'" & Regresacuenta(OtraRPPD1) & "', "
        Sql &= "	" & RegresaNivel(NivelOtraRP1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoOtraRP1) & ", "
        Sql &= "	" & Eventos.Bool2(Negativo1) & ", "
        Sql &= "	'" & Regresacuenta(AnticipoGPUE1) & "', "
        Sql &= "	" & RegresaNivel(NivelA1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoA1) & ", "
        Sql &= "	'" & Regresacuenta(AnticipoEPUE1) & "', "
        Sql &= "	" & RegresaNivel(NivelAE1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoAE1) & ", "
        Sql &= "	'" & Regresacuenta(IVAAPUE1) & "', "
        Sql &= "	" & RegresaNivel(NivelIA1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoIA1) & ", "
        Sql &= "	'" & Regresacuenta(AnticipoGPPD1) & "', "
        Sql &= "	" & RegresaNivel(NivelAP1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoAP1) & ", "
        Sql &= "	'" & Regresacuenta(AnticipoEPPD1) & "', "
        Sql &= "	" & RegresaNivel(NivelAEP1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoAEP1) & " , "
        Sql &= "	'" & Regresacuenta(IVAAPPD1) & "', "
        Sql &= "	" & RegresaNivel(NivelIAP1) & " , "
        Sql &= "	" & Eventos.Bool2(CargoIAP1) & " , "
        Sql &= "	'" & Regresacuenta(IVAPA1) & "', "
        Sql &= "	" & RegresaNivel(NivelIVAPA1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoIVAPA1) & " , "
        Sql &= "	'" & Regresacuenta(Debe1) & "', "
        Sql &= "	" & RegresaNivel(NivelD1) & " , "
        Sql &= "	" & Eventos.Bool2(CargoD1) & " , "
        Sql &= "	'" & Regresacuenta(COGC1) & "', "
        Sql &= "	" & RegresaNivel(NivelCOGC1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoCOGC1) & " , "
        Sql &= "	'" & Regresacuenta(COGA1) & "', "
        Sql &= "	" & RegresaNivel(NivelCOGA1) & " , "
        Sql &= "	" & Eventos.Bool2(CargoCOGA1) & " , "
        Sql &= "	'" & Regresacuenta(COEC1) & "', "
        Sql &= "	" & RegresaNivel(NivelCOEC1) & ", "
        Sql &= "	" & Eventos.Bool2(CargoCOEC1) & " , "
        Sql &= "	'" & Regresacuenta(COEA1) & "', "
        Sql &= "	" & RegresaNivel(NivelCOEA1) & " , "
        Sql &= "	" & Eventos.Bool2(CargoCOEA1) & ",  "
        Sql &= "	'" & Regresacuenta(DebeE1) & "', "
        Sql &= "	" & RegresaNivel(NivelDE1) & " , "
        Sql &= "	" & Eventos.Bool2(CargoDE1) & "  "
        Sql &= "	) "
        If Eventos.Comando_sql(Sql) > 0 Then
            Eventos.Insertar_usuariol("Ins Eg Clv", Sql)
            Me.Tabla.Item(Id.Index, Me.Tabla.CurrentRow.Index).Value = Convert.ToInt32(Eventos.Obtener_DS("SELECT ident_current('ClaveEgresos')").Tables(0).Rows(0)(0))
        End If
    End Sub
    Private Function Regresacuenta(ByVal Cadena As String)

        If Cadena = "" Then
            Regresacuenta = ""
        Else
            Regresacuenta = Cadena.Replace("-", "").Substring(0, 16)
        End If

        Return Regresacuenta
    End Function
    Private Function RegresaNivel(ByVal Nivel As String)
        If Nivel = "" Then
            RegresaNivel = 0
        Else
            RegresaNivel = Convert.ToInt32(Nivel)
        End If

        Return RegresaNivel
    End Function

    Private Sub Editar(ByVal Id_Clave As Integer, ByVal Id_Cliente As Integer, ByVal li As String, ByVal Concepto As String, ByVal Descripcion1 As String, ByVal Clave1 As String, ByVal Tasa1 As Decimal, ByVal GravadoPUE1 As String, ByVal NivelG1 As String, ByVal CargoG1 As Boolean, ByVal ExentoPUE1 As String,
        ByVal NivelE1 As String, ByVal CargoE1 As Boolean, ByVal IVAPUE1 As String, ByVal NivelI1 As String, ByVal CargoI1 As Boolean, ByVal RISRPUE1 As String, ByVal NivelRISR1 As String, ByVal CargoRISR1 As Boolean,
                         ByVal RIVAPUE1 As String, ByVal NivelRIVA1 As String, ByVal CargoRIVA1 As Boolean, ByVal OtraRPUE1 As String, ByVal NivelOtraR1 As String, ByVal CargoOtraR1 As Boolean, ByVal GravadoPPD1 As String, ByVal NivelGP1 As String, ByVal CargoGP1 As Boolean, ByVal ExentoPPD1 As String,
         ByVal NivelEP1 As String, ByVal CargoEP1 As Boolean, ByVal IVAPPD1 As String, ByVal NivelIP1 As String, ByVal CargoIP1 As Boolean, ByVal RISRPPD1 As String, ByVal NivelRISRP1 As String, ByVal CargoRISRP1 As Boolean, ByVal RIVAPPD1 As String,
         ByVal NivelRIVAP1 As String, ByVal CargoRIVAP1 As Boolean, ByVal OtraRPPD1 As String, ByVal NivelOtraRP1 As String, ByVal CargoOtraRP1 As Boolean, ByVal Negativo1 As Boolean, ByVal AnticipoGPUE1 As String,
         ByVal NivelA1 As String, ByVal CargoA1 As Boolean, ByVal AnticipoEPUE1 As String, ByVal NivelAE1 As String, ByVal CargoAE1 As Boolean, ByVal IVAAPUE1 As String, ByVal NivelIA1 As String, ByVal CargoIA1 As Boolean, ByVal AnticipoGPPD1 As String,
         ByVal NivelAP1 As String, ByVal CargoAP1 As Boolean, ByVal AnticipoEPPD1 As String, ByVal NivelAEP1 As String, ByVal CargoAEP1 As Boolean, ByVal IVAAPPD1 As String, ByVal NivelIAP1 As String, ByVal CargoIAP1 As Boolean,
         ByVal IVAPA1 As String, ByVal NivelIVAPA1 As String, ByVal CargoIVAPA1 As Boolean, ByVal Debe1 As String, ByVal NivelD1 As String, ByVal CargoD1 As Boolean,
         ByVal COGC1 As String, ByVal NivelCOGC1 As String, ByVal CargoCOGC1 As Boolean, ByVal COGA1 As String, ByVal NivelCOGA1 As String, ByVal CargoCOGA1 As Boolean, ByVal COEC1 As String, ByVal NivelCOEC1 As String, ByVal CargoCOEC1 As Boolean,
         ByVal COEA1 As String, ByVal NivelCOEA1 As String, ByVal CargoCOEA1 As Boolean, ByVal DebeE1 As String, ByVal NivelDE1 As String, ByVal CargoDE1 As Boolean)

        Concepto = Limitar(Concepto)
        Dim Sql As String = "UPDATE dbo.ClaveEgresos SET  Ligar = '" & li & "',	Concepto = '" & Concepto & "',	Descripcion = '" & Descripcion1 & "',	Clave = '" & Clave1 & "',	Tasa = " & Tasa1 & ",	GravadoPUE = '" & Regresacuenta(GravadoPUE1) & "',	NivelG = " & RegresaNivel(NivelG1) & ",
	CargoG = " & Eventos.Bool2(CargoG1) & ",	ExentoPUE ='" & Regresacuenta(ExentoPUE1) & "',	NivelE = " & RegresaNivel(NivelE1) & ",	CargoE = " & Eventos.Bool2(CargoE1) & ",	IVAPUE = '" & Regresacuenta(IVAPUE1) & "',	NivelI = " & RegresaNivel(NivelI1) & ",	CargoI = " & Eventos.Bool2(CargoI1) & ",
	RISRPUE = '" & Regresacuenta(RISRPUE1) & "',	NivelRISR = " & RegresaNivel(NivelRISR1) & ",	CargoRISR = " & Eventos.Bool2(CargoRISR1) & ",	RIVAPUE = '" & Regresacuenta(RIVAPUE1) & "',	NivelRIVA = " & RegresaNivel(NivelRIVA1) & ",	CargoRIVA = " & Eventos.Bool2(CargoRIVA1) & ",
	OtraRPUE = '" & Regresacuenta(OtraRPUE1) & "',	NivelOtraR =" & RegresaNivel(NivelOtraR1) & ",	CargoOtraR = " & Eventos.Bool2(CargoOtraR1) & ",	GravadoPPD = '" & Regresacuenta(GravadoPPD1) & "',	NivelGP = " & RegresaNivel(NivelGP1) & ",	CargoGP = " & Eventos.Bool2(CargoGP1) & ",
	ExentoPPD = '" & Regresacuenta(ExentoPPD1) & "',	NivelEP = " & RegresaNivel(NivelEP1) & ",	CargoEP = " & Eventos.Bool2(CargoEP1) & ",	IVAPPD = '" & Regresacuenta(IVAPPD1) & "',	NivelIP = " & RegresaNivel(NivelIP1) & ",	CargoIP = " & Eventos.Bool2(CargoIP1) & ",	RISRPPD = '" & Regresacuenta(RISRPPD1) & "',
	NivelRISRP = " & RegresaNivel(NivelRISRP1) & ",	CargoRISRP = " & Eventos.Bool2(CargoRISRP1) & ",	RIVAPPD = '" & Regresacuenta(RIVAPPD1) & "',	NivelRIVAP = " & RegresaNivel(NivelRIVAP1) & ",	CargoRIVAP = " & Eventos.Bool2(CargoRIVAP1) & ",	OtraRPPD = '" & Regresacuenta(OtraRPPD1) & "',
	NivelOtraRP = " & RegresaNivel(NivelOtraRP1) & ",	CargoOtraRP = " & Eventos.Bool2(CargoOtraRP1) & ",	Negativo = " & Eventos.Bool2(Negativo1) & ",	AnticipoGPUE = '" & Regresacuenta(AnticipoGPUE1) & "',	NivelA =" & RegresaNivel(NivelA1) & ",	CargoA = " & Eventos.Bool2(CargoA1) & ",
	AnticipoEPUE = '" & Regresacuenta(AnticipoEPUE1) & "',	NivelAE = " & RegresaNivel(NivelAE1) & ",	CargoAE = " & Eventos.Bool2(CargoAE1) & ",	IVAAPUE = '" & Regresacuenta(IVAAPUE1) & "',	NivelIA = " & RegresaNivel(NivelIA1) & ",	CargoIA = " & Eventos.Bool2(CargoIA1) & ",
	AnticipoGPPD = '" & Regresacuenta(AnticipoGPPD1) & "',	NivelAP = " & RegresaNivel(NivelAP1) & ",	CargoAP = " & Eventos.Bool2(CargoAP1) & ",	AnticipoEPPD = '" & Regresacuenta(AnticipoEPPD1) & "',	NivelAEP =" & RegresaNivel(NivelAEP1) & ",	CargoAEP = " & Eventos.Bool2(CargoAEP1) & ",
	IVAAPPD = '" & Regresacuenta(IVAAPPD1) & "',	NivelD = " & RegresaNivel(NivelD1) & ",	CargoIAP = " & Eventos.Bool2(CargoIAP1) & ",IVAPA = '" & Regresacuenta(IVAPA1) & "',	NivelIVAPA = " & RegresaNivel(NivelIVAPA1) & ",	CargoIVAPA = " & Eventos.Bool2(CargoIVAPA1) & ",    
    Debe = '" & Regresacuenta(Debe1) & "',	NivelIAP = " & RegresaNivel(NivelIAP1) & ",	CargoD = " & Eventos.Bool2(CargoD1) & ",         COGC = '" & Regresacuenta(COGC1) & "',	NivelCOGC = " & RegresaNivel(NivelCOGC1) & ",	CargoCOGC = " & Eventos.Bool2(CargoCOGC1) & ", 
        COGA = '" & Regresacuenta(COGA1) & "',	NivelCOGA = " & RegresaNivel(NivelCOGA1) & ",	CargoCOGA = " & Eventos.Bool2(CargoCOGA1) & ",         COEC = '" & Regresacuenta(COEC1) & "',	NivelCOEC = " & RegresaNivel(NivelCOEC1) & ",	CargoCOEC = " & Eventos.Bool2(CargoCOEC1) & ", 
            COEA = '" & Regresacuenta(COEA1) & "',	NivelCOEA = " & RegresaNivel(NivelCOEA1) & ",	CargoCOEA = " & Eventos.Bool2(CargoCOEA1) & "  ,   DebeE = '" & Regresacuenta(DebeE1) & "',	NivelDE = " & RegresaNivel(NivelDE1) & ",	CargoDE = " & Eventos.Bool2(CargoDE1) & "    where Id_Clave = " & Id_Clave & " and Id_Empresa = " & Me.lstCliente.SelectItem & " "

        If Eventos.Comando_sql(Sql) > 0 Then
            Eventos.Insertar_usuariol("Ins Eg Clv", Sql)
        End If
    End Sub
    Private Function Limitar(ByVal Cadena As String)
        If Len(Cadena) > 13 Then
            Limitar = Cadena.Substring(0, 12)
        Else
            Limitar = Cadena
        End If
        Return Limitar
    End Function

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.Tabla.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In Tabla.Rows
                For j As Integer = 0 To Me.Tabla.Columns.Count - 1
                    If Me.Tabla.Item(j, Fila.Index).Selected = True Then
                        Me.Tabla.Item(j, Fila.Index).Value = ""
                    End If
                Next
            Next
        End If
    End Sub
End Class
