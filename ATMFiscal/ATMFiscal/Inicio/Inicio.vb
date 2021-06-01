Imports Telerik.WinControls

Public Class Inicio
    Dim activo As Boolean
    Public Clt As Integer
    Private Sub Inicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Me.MenuModulos.Enabled = False
        Me.MenuUsuarios.Enabled = False
        Me.MenuVentanas.Enabled = False
        Me.MenuEmpresa.Enabled = False
        Me.MenuInicioSesion.Shortcuts.Add(New RadShortcut(Keys.Control, Keys.I))
        Me.MenuRecibidas.Shortcuts.Add(New RadShortcut(Keys.Control, Keys.R))
        Me.MenuEmitidas.Shortcuts.Add(New RadShortcut(Keys.Control, Keys.E))
        Me.MenuControlDePolizas.Shortcuts.Add(New RadShortcut(Keys.Control, Keys.P))

        activo = False
    End Sub

    Private Sub MenuInicioSesion_Click(sender As Object, e As EventArgs) Handles MenuInicioSesion.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.MenuInicio.CheckOnClick = True Then
            RadMessageBox.Show("Usted ya inicio sesion...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Else
            Acceso.ShowDialog()
        End If
    End Sub

    Private Sub MenuSalir_Click(sender As Object, e As EventArgs) Handles MenuSalir.Click
        Application.Exit()
    End Sub


    Private Sub MenuPersonal_Click(sender As Object, e As EventArgs) Handles MenuPersonal.Click
        Eventos.Abrir_form(Control_del_Personal)
    End Sub

    Private Sub MenUsuarios_Click(sender As Object, e As EventArgs) Handles MenUsuarios.Click
        Eventos.Abrir_form(Control_de_Usuarios)
    End Sub

    Private Sub MenuDepartamentos_Click(sender As Object, e As EventArgs) Handles MenuDepartamentos.Click
        Eventos.Abrir_form(Control_Depto_trabajo)
    End Sub

    Private Sub MenuAsignacionDep_Click(sender As Object, e As EventArgs) Handles MenuAsignacionDep.Click
        Eventos.Abrir_form(Designar_Usuarios_Equipos)
    End Sub

    Private Sub MenuAsignacionEmpresas_Click(sender As Object, e As EventArgs) Handles MenuAsignacionEmpresas.Click
        Eventos.Abrir_form(Control_Proyectos)
    End Sub

    Private Sub MCempresa_Click(sender As Object, e As EventArgs) Handles MCempresa.Click
        Eventos.Abrir_form(Control_de_Empresas)
    End Sub

    Private Sub MenuSeries_Click(sender As Object, e As EventArgs) Handles MenuSeries.Click
        Eventos.Abrir_form(Ingresos_Series)
    End Sub

    Private Sub MenuBancos_Click(sender As Object, e As EventArgs) Handles MenuBancos.Click
        Eventos.Abrir_form(Control_Bancos)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Eventos.Abrir_form(ResetPass)
    End Sub


    Private Sub MenuRecibidas_Click(sender As Object, e As EventArgs) Handles MenuRecibidas.Click
        Eventos.Abrir_form(ControldeEgresos)
    End Sub

    Private Sub MenuFormasPago_Click(sender As Object, e As EventArgs) Handles MenuFormasPago.Click
        Eventos.Abrir_form(Formas_de_Pago)
    End Sub

    Private Sub MenuTasas_Click(sender As Object, e As EventArgs) Handles MenuTasas.Click
        Eventos.Abrir_form(Control_Tasas)
    End Sub

    Private Sub MenuMetodoPago_Click(sender As Object, e As EventArgs) Handles MenuMetodoPago.Click
        Eventos.Abrir_form(Metodos_de_Pago)
    End Sub

    Private Sub MenuPolSat_Click(sender As Object, e As EventArgs) Handles MenuPolSat.Click
        Eventos.Abrir_form(Tipo_Poliza_SAT)
    End Sub

    Private Sub MenuPolizasInfNoUsar_Click(sender As Object, e As EventArgs) Handles MenuPolizasInfNoUsar.Click
        Eventos.Abrir_form(Tipo_Polizas)
    End Sub

    Private Sub MenuUsoCFDI_Click(sender As Object, e As EventArgs) Handles MenuUsoCFDI.Click
        Eventos.Abrir_form(Uso_CFDI)
    End Sub

    Private Sub MenuTipoCambio_Click(sender As Object, e As EventArgs) Handles MenuTipoCambio.Click
        Eventos.Abrir_form(Tipos_de_Cambio)
    End Sub

    Private Sub MenuClasificacionDeCuentas_Click(sender As Object, e As EventArgs) Handles MenuClasificacionDeCuentas.Click
        Eventos.Abrir_form(Clasificaciondecuentas)
    End Sub

    Private Sub MenuPrecargaRcibidas_Click(sender As Object, e As EventArgs) Handles MenuPrecargaRcibidas.Click
        Eventos.Abrir_form(Polizas_Modelo_Recibidas)
    End Sub

    Private Sub MenuParcialidadesRecibidas_Click(sender As Object, e As EventArgs) Handles MenuParcialidadesRecibidas.Click
        Eventos.Abrir_form(Parcialidades)
    End Sub

    Private Sub MenuAsignacionLetrasCont_Click(sender As Object, e As EventArgs)
        Eventos.Abrir_form(Letras_Contabilizacion)
    End Sub

    Private Sub MenuEmitidas_Click(sender As Object, e As EventArgs) Handles MenuEmitidas.Click
        Eventos.Abrir_form(Emitidas)
    End Sub

    Private Sub MenuBancosEmitidas_Click(sender As Object, e As EventArgs) Handles MenuBancosEmitidas.Click
        Eventos.Abrir_form(BancosRFC)
    End Sub

    Private Sub MenuXMLComplemento_Click(sender As Object, e As EventArgs) Handles MenuXMLComplemento.Click
        Eventos.Abrir_form(Importar_Complementos)
    End Sub

    Private Sub MenuXMLNormal_Click(sender As Object, e As EventArgs) Handles MenuXMLNormal.Click
        Eventos.Abrir_form(Importar_XML_DB)
    End Sub

    Private Sub MenuPolizasModelo_Click(sender As Object, e As EventArgs) Handles MenuPolizasModelo.Click
        Eventos.Abrir_form(Poliza_Modelo)
    End Sub

    Private Sub MenuLectorDeXML_Click(sender As Object, e As EventArgs) Handles MenuLectorDeXML.Click
        Eventos.Abrir_form(Codificar_XML)
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Eventos.Abrir_form(ClavesEgresos)
    End Sub

    Private Sub RadMenuItem10_Click(sender As Object, e As EventArgs) Handles RadMenuItem10.Click
        Eventos.Abrir_form(Cuentas_Cero)
    End Sub

    Private Sub RadMenuItem11_Click(sender As Object, e As EventArgs) Handles RadMenuItem11.Click
        Eventos.Abrir_form(Cargos_Cero)
    End Sub

    Private Sub RadMenuItem15_Click(sender As Object, e As EventArgs) Handles RadMenuItem15.Click
        Eventos.Abrir_form(Calculo_de_Impuestos)
    End Sub


    Private Sub RadMenuItem14_Click(sender As Object, e As EventArgs) Handles RadMenuItem14.Click
        Eventos.Abrir_form(Cuentas_Obligaciones)
    End Sub

    Private Sub RadMenuItem13_Click(sender As Object, e As EventArgs) Handles RadMenuItem13.Click
        Eventos.Abrir_form(Obligaciones)
    End Sub

    Private Sub RadMenuItem16_Click(sender As Object, e As EventArgs) Handles RadMenuItem16.Click
        Eventos.Abrir_form(ImpuestosPM)
    End Sub

    Private Sub RadMenuItem19_Click(sender As Object, e As EventArgs) Handles RadMenuItem19.Click
        Eventos.Abrir_form(Auxiliares)
    End Sub

    Private Sub RadMenuItem20_Click(sender As Object, e As EventArgs) Handles RadMenuItem20.Click
        Eventos.Abrir_form(Balance)
    End Sub

    Private Sub RadMenuItem21_Click(sender As Object, e As EventArgs) Handles RadMenuItem21.Click
        Eventos.Abrir_form(Balanza)
    End Sub

    Private Sub RadMenuItem23_Click(sender As Object, e As EventArgs) Handles RadMenuItem23.Click
        Eventos.Abrir_form(Cargar_Saldos_Iniciales)
    End Sub

    Private Sub RadMenuItem24_Click(sender As Object, e As EventArgs) Handles RadMenuItem24.Click
        Eventos.Abrir_form(Diario)
    End Sub

    Private Sub RadMenuItem25_Click(sender As Object, e As EventArgs) Handles RadMenuItem25.Click
        Eventos.Abrir_form(EstadodeResultados)
    End Sub

    Private Sub MenuAuditorXML_Click(sender As Object, e As EventArgs) Handles MenuAuditorXML.Click
        Eventos.Abrir_form(ReportesXml)
    End Sub

    Private Sub RadMenuItem26_Click(sender As Object, e As EventArgs) Handles RadMenuItem26.Click
        Eventos.Abrir_form(DIOT)
    End Sub

    Private Sub MenuCancelacionesDeIVA_Click(sender As Object, e As EventArgs) Handles MenuCancelacionesDeIVA.Click
        Eventos.Abrir_form(CancelacionesdeIVAClientes)
    End Sub
    Private Sub RadMenuItem28_Click(sender As Object, e As EventArgs) Handles RadMenuItem28.Click
        Eventos.Abrir_form(Control_Masivo_Personal)
    End Sub

    Private Sub RadMenuItem29_Click(sender As Object, e As EventArgs) Handles RadMenuItem29.Click
        Eventos.Abrir_form(Leector_de_Archivo_IMSS)
    End Sub

    Private Sub RadMenuItem32_Click(sender As Object, e As EventArgs) Handles RadMenuItem32.Click
        Eventos.Abrir_form(Claves)
    End Sub

    Private Sub RadMenuItem33_Click(sender As Object, e As EventArgs) Handles RadMenuItem33.Click
        Eventos.Abrir_form(TipoActivos)
    End Sub
    Private Sub RadMenuItem34_Click(sender As Object, e As EventArgs) Handles RadMenuItem34.Click
        Eventos.Abrir_form(Depreciacion)
    End Sub
    Private Sub RadMenuItem35_Click(sender As Object, e As EventArgs) Handles RadMenuItem35.Click
        Eventos.Abrir_form(VentadeActivos)
    End Sub

    Private Sub RadMenuItem36_Click(sender As Object, e As EventArgs) Handles RadMenuItem36.Click
        Eventos.Abrir_form(VentadeTerrenos)
    End Sub

    Private Sub RadMenuItem37_Click(sender As Object, e As EventArgs) Handles RadMenuItem37.Click
        Eventos.Abrir_form(ResumenCruces)
    End Sub

    Private Sub RadMenuItem38_Click(sender As Object, e As EventArgs) Handles RadMenuItem38.Click
        Eventos.Abrir_form(AuditorRegistrosXML)
    End Sub

    Private Sub RadMenuItem17_Click(sender As Object, e As EventArgs) Handles RadMenuItem17.Click
        Eventos.Abrir_form(ImpuestosPM)
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Eventos.Abrir_form(Depreciacion_Anual_Activos)
    End Sub

    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs) Handles RadMenuItem6.Click
        Eventos.Abrir_form(Cuentas_Activos_Clientes)
    End Sub
    Private Sub RadMenuItem40_Click(sender As Object, e As EventArgs) Handles RadMenuItem40.Click
        Eventos.Abrir_form(Cuentas_Depreciacion)
    End Sub
    Private Sub RadMenuItem39_Click(sender As Object, e As EventArgs) Handles RadMenuItem39.Click
        Eventos.Abrir_form(Cuentas_MOI)
    End Sub
    Private Sub MenuCatalogoDeCuentas_Click(sender As Object, e As EventArgs) Handles MenuCatalogoDeCuentas.Click
        Eventos.Abrir_form(Catalogo_Cuentas)
    End Sub
    Private Sub MenuE_Click(sender As Object, e As EventArgs) Handles MenuE.Click
        Eventos.Abrir_form(Exportar_Polizas)
    End Sub
    Private Sub MenuControlProveedores_Click(sender As Object, e As EventArgs) Handles MenuControlProveedores.Click
        Eventos.Abrir_form(Importar_Polizas)
    End Sub

    Private Sub RadMenuItem41_Click(sender As Object, e As EventArgs) Handles RadMenuItem41.Click
        Eventos.Abrir_form(Cruces_Saldos)
    End Sub



    Private Sub RadMenuItem42_Click(sender As Object, e As EventArgs) Handles RadMenuItem42.Click
        Eventos.Abrir_form(CancelacionIva)
    End Sub

    Private Sub RadMenuItem43_Click(sender As Object, e As EventArgs) Handles RadMenuItem43.Click
        Eventos.Abrir_form(MasivosClietesObligaciones)
    End Sub

    Private Sub RadMenuItem44_Click(sender As Object, e As EventArgs) Handles RadMenuItem44.Click
        Eventos.Abrir_form(ContabilizadordeGastos)
    End Sub

    Private Sub RadMenuItem45_Click(sender As Object, e As EventArgs) Handles RadMenuItem45.Click
        Eventos.Abrir_form(LiberarParcialidades)
    End Sub

    Private Sub RadMenuItem46_Click(sender As Object, e As EventArgs) Handles RadMenuItem46.Click
        Eventos.Abrir_form(ReporteXml)
    End Sub

    Private Sub RadMenuItem47_Click(sender As Object, e As EventArgs) Handles RadMenuItem47.Click
        Eventos.Abrir_form(Cuentas_Diot)
    End Sub

    Private Sub RadMenuItem48_Click(sender As Object, e As EventArgs) Handles RadMenuItem48.Click
        Eventos.Abrir_form(CuentasContablesFiscales)
    End Sub

    Private Sub RadMenuItem49_Click(sender As Object, e As EventArgs) Handles RadMenuItem49.Click
        Eventos.Abrir_form(ClavesEgresosGastos)
    End Sub

    Private Sub MenuComplementosSinFacturaEmitidas_Click(sender As Object, e As EventArgs) Handles MenuComplementosSinFacturaEmitidas.Click
        Eventos.Abrir_form(ComplementosSinFactura)
    End Sub

    Private Sub MenuEliminarXML_Click(sender As Object, e As EventArgs) Handles MenuEliminarXML.Click
        Eventos.Abrir_form(EliminaXML)
    End Sub

    Private Sub RadMenuItem50_Click(sender As Object, e As EventArgs)
        Eventos.Abrir_form(CodificadorGastos)
    End Sub

    Private Sub RadMenuItem51_Click(sender As Object, e As EventArgs) Handles RadMenuItem51.Click
        Eventos.Abrir_form(LiberarCarga)
    End Sub

    Private Sub RadMenuItem52_Click(sender As Object, e As EventArgs) Handles RadMenuItem52.Click
        Eventos.Abrir_form(LiberadorPolizasEgresos)
    End Sub

    Private Sub RadMenuItem53_Click(sender As Object, e As EventArgs) Handles RadMenuItem53.Click
        ' Eventos.Abrir_Capeta(CatalogoSumas)
    End Sub
    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Eventos.Abrir_form(Control_de_Polizas)
    End Sub

    Private Sub MenuControlDePolizas_Click(sender As Object, e As EventArgs) Handles MenuControlDePolizas.Click
        Eventos.Abrir_form(Control_de_Polizas)
    End Sub

    Private Sub RadMenuItem55_Click(sender As Object, e As EventArgs) Handles RadMenuItem55.Click
        Eventos.Abrir_form(ListaNegra)
    End Sub

    Private Sub RadMenuItem54_Click(sender As Object, e As EventArgs) Handles RadMenuItem54.Click
        Eventos.Abrir_form(Contabilizacion_Digital)
    End Sub

    Private Sub MenuClavesSat_Click(sender As Object, e As EventArgs) Handles MenuClavesSat.Click
        Eventos.Abrir_form(Claves_Sat)
    End Sub

    Private Sub RadMenuItem12_Click(sender As Object, e As EventArgs) Handles RadMenuItem12.Click
        Eventos.Abrir_form(Abonos_Cero)
    End Sub

    Private Sub RadMenuItem56_Click(sender As Object, e As EventArgs) Handles RadMenuItem56.Click
        Eventos.Abrir_form(Reclasificador)
    End Sub

    Private Sub RadMenuItem57_Click(sender As Object, e As EventArgs) Handles RadMenuItem57.Click
        Eventos.Abrir_form(ImportarCatalogos)
    End Sub

End Class
