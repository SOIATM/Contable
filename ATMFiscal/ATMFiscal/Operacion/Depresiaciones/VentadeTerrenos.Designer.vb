<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VentadeTerrenos
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VentadeTerrenos))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.LstAnio = New Telerik.WinControls.UI.RadDropDownList()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmdPoliza = New Telerik.WinControls.UI.RadButton()
        Me.CmdCalcular = New Telerik.WinControls.UI.RadButton()
        Me.CmdGuardar = New Telerik.WinControls.UI.RadButton()
        Me.CmdAgregar = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.Aplicar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.UUID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha_Adquisicion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Precio_Adquisicion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Referencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha_de_Venta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Precio_Venta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaPago = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Utilidad_Contable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaUtilidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Perdida_Contable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaPerdida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UltimoMes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImporteUltimomes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha_Adq = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INPC_Adquisicion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Factor_Actualizacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Precio_Adq_Actualizado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Precio_Venta_Fiscal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Utilidad_Fiscal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Perdida_Fiscal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TipoPoliza = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Numero = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Año = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tipo = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Bo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaOrigen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaDestino = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaMov = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nocheque = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.LstAnio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdPoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdCalcular, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdAgregar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.LstAnio)
        Me.RadPanel1.Controls.Add(Me.Label1)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.CmdPoliza)
        Me.RadPanel1.Controls.Add(Me.CmdCalcular)
        Me.RadPanel1.Controls.Add(Me.CmdGuardar)
        Me.RadPanel1.Controls.Add(Me.CmdAgregar)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(835, 88)
        Me.RadPanel1.TabIndex = 0
        '
        'LstAnio
        '
        Me.LstAnio.Location = New System.Drawing.Point(680, 33)
        Me.LstAnio.Name = "LstAnio"
        Me.LstAnio.Size = New System.Drawing.Size(125, 36)
        Me.LstAnio.TabIndex = 662
        Me.LstAnio.Text = " "
        Me.LstAnio.ThemeName = "Material"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(677, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 18)
        Me.Label1.TabIndex = 661
        Me.Label1.Text = "Año:"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(294, 33)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(370, 36)
        Me.lstCliente.TabIndex = 659
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(290, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 658
        Me.Label3.Text = "Empresa:"
        '
        'CmdPoliza
        '
        Me.CmdPoliza.Image = Global.ATMFiscal.My.Resources.Resources.Poliza
        Me.CmdPoliza.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdPoliza.Location = New System.Drawing.Point(220, 9)
        Me.CmdPoliza.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdPoliza.Name = "CmdPoliza"
        Me.CmdPoliza.Size = New System.Drawing.Size(50, 54)
        Me.CmdPoliza.TabIndex = 657
        Me.CmdPoliza.TabStop = False
        Me.CmdPoliza.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdPoliza.ThemeName = "Aqua"
        '
        'CmdCalcular
        '
        Me.CmdCalcular.Image = Global.ATMFiscal.My.Resources.Resources.Calculadora
        Me.CmdCalcular.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCalcular.Location = New System.Drawing.Point(112, 9)
        Me.CmdCalcular.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdCalcular.Name = "CmdCalcular"
        Me.CmdCalcular.Size = New System.Drawing.Size(50, 54)
        Me.CmdCalcular.TabIndex = 656
        Me.CmdCalcular.TabStop = False
        Me.CmdCalcular.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdCalcular.ThemeName = "Aqua"
        '
        'CmdGuardar
        '
        Me.CmdGuardar.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardar.Location = New System.Drawing.Point(166, 9)
        Me.CmdGuardar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardar.Name = "CmdGuardar"
        Me.CmdGuardar.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardar.TabIndex = 655
        Me.CmdGuardar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardar.ThemeName = "Aqua"
        '
        'CmdAgregar
        '
        Me.CmdAgregar.Image = Global.ATMFiscal.My.Resources.Resources.añadir_fila
        Me.CmdAgregar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdAgregar.Location = New System.Drawing.Point(58, 9)
        Me.CmdAgregar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdAgregar.Name = "CmdAgregar"
        Me.CmdAgregar.Size = New System.Drawing.Size(50, 54)
        Me.CmdAgregar.TabIndex = 651
        Me.CmdAgregar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdAgregar.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(4, 9)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 649
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TablaImportar.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Aplicar, Me.UUID, Me.Cliente, Me.Fecha_Adquisicion, Me.Precio_Adquisicion, Me.Referencia, Me.Fecha_de_Venta, Me.Precio_Venta, Me.CtaPago, Me.Utilidad_Contable, Me.CtaUtilidad, Me.Perdida_Contable, Me.CtaPerdida, Me.UltimoMes, Me.ImporteUltimomes, Me.Fecha_Adq, Me.INPC_Adquisicion, Me.Factor_Actualizacion, Me.Precio_Adq_Actualizado, Me.Precio_Venta_Fiscal, Me.Utilidad_Fiscal, Me.Perdida_Fiscal, Me.TipoPoliza, Me.Numero, Me.Año, Me.Mes, Me.Tipo, Me.Bo, Me.CtaOrigen, Me.BD, Me.CtaDestino, Me.FechaMov, Me.Nocheque})
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.TablaImportar.Location = New System.Drawing.Point(0, 88)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaImportar.Name = "TablaImportar"
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TablaImportar.RowsDefaultCellStyle = DataGridViewCellStyle15
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(835, 184)
        Me.TablaImportar.TabIndex = 6
        '
        'Aplicar
        '
        Me.Aplicar.Frozen = True
        Me.Aplicar.HeaderText = "Listo"
        Me.Aplicar.Name = "Aplicar"
        '
        'UUID
        '
        DataGridViewCellStyle2.NullValue = " "
        Me.UUID.DefaultCellStyle = DataGridViewCellStyle2
        Me.UUID.Frozen = True
        Me.UUID.HeaderText = "UUID"
        Me.UUID.Name = "UUID"
        '
        'Cliente
        '
        DataGridViewCellStyle3.NullValue = " "
        Me.Cliente.DefaultCellStyle = DataGridViewCellStyle3
        Me.Cliente.Frozen = True
        Me.Cliente.HeaderText = "Cliente"
        Me.Cliente.Name = "Cliente"
        '
        'Fecha_Adquisicion
        '
        Me.Fecha_Adquisicion.Frozen = True
        Me.Fecha_Adquisicion.HeaderText = "Fecha Adquisicion"
        Me.Fecha_Adquisicion.Name = "Fecha_Adquisicion"
        '
        'Precio_Adquisicion
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = "0.00"
        Me.Precio_Adquisicion.DefaultCellStyle = DataGridViewCellStyle4
        Me.Precio_Adquisicion.HeaderText = "Precio Adquisicion"
        Me.Precio_Adquisicion.Name = "Precio_Adquisicion"
        '
        'Referencia
        '
        Me.Referencia.HeaderText = "Cuenta Terreno"
        Me.Referencia.Name = "Referencia"
        '
        'Fecha_de_Venta
        '
        Me.Fecha_de_Venta.HeaderText = "Fecha de Venta"
        Me.Fecha_de_Venta.Name = "Fecha_de_Venta"
        '
        'Precio_Venta
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.Precio_Venta.DefaultCellStyle = DataGridViewCellStyle5
        Me.Precio_Venta.HeaderText = "Precio Venta"
        Me.Precio_Venta.Name = "Precio_Venta"
        '
        'CtaPago
        '
        Me.CtaPago.HeaderText = "Cuenta de Pago"
        Me.CtaPago.Name = "CtaPago"
        '
        'Utilidad_Contable
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = "0"
        Me.Utilidad_Contable.DefaultCellStyle = DataGridViewCellStyle6
        Me.Utilidad_Contable.HeaderText = "Utilidad Contable"
        Me.Utilidad_Contable.Name = "Utilidad_Contable"
        '
        'CtaUtilidad
        '
        Me.CtaUtilidad.HeaderText = "Cuenta Utilidad"
        Me.CtaUtilidad.Name = "CtaUtilidad"
        '
        'Perdida_Contable
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = "0"
        Me.Perdida_Contable.DefaultCellStyle = DataGridViewCellStyle7
        Me.Perdida_Contable.HeaderText = "Perdida Contable"
        Me.Perdida_Contable.Name = "Perdida_Contable"
        '
        'CtaPerdida
        '
        Me.CtaPerdida.HeaderText = "Cuenta Perdida"
        Me.CtaPerdida.Name = "CtaPerdida"
        '
        'UltimoMes
        '
        Me.UltimoMes.HeaderText = "Ultimo Mes"
        Me.UltimoMes.Name = "UltimoMes"
        '
        'ImporteUltimomes
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = "0"
        Me.ImporteUltimomes.DefaultCellStyle = DataGridViewCellStyle8
        Me.ImporteUltimomes.HeaderText = "Importe Ultimo Mes"
        Me.ImporteUltimomes.Name = "ImporteUltimomes"
        '
        'Fecha_Adq
        '
        Me.Fecha_Adq.HeaderText = "Fecha Adq"
        Me.Fecha_Adq.Name = "Fecha_Adq"
        '
        'INPC_Adquisicion
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = "0"
        Me.INPC_Adquisicion.DefaultCellStyle = DataGridViewCellStyle9
        Me.INPC_Adquisicion.HeaderText = "INPC Adquisicion"
        Me.INPC_Adquisicion.Name = "INPC_Adquisicion"
        '
        'Factor_Actualizacion
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N2"
        DataGridViewCellStyle10.NullValue = "0"
        Me.Factor_Actualizacion.DefaultCellStyle = DataGridViewCellStyle10
        Me.Factor_Actualizacion.HeaderText = "Factor Actualizacion"
        Me.Factor_Actualizacion.Name = "Factor_Actualizacion"
        '
        'Precio_Adq_Actualizado
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N2"
        DataGridViewCellStyle11.NullValue = "0"
        Me.Precio_Adq_Actualizado.DefaultCellStyle = DataGridViewCellStyle11
        Me.Precio_Adq_Actualizado.HeaderText = "Precio Adq Actualizado"
        Me.Precio_Adq_Actualizado.Name = "Precio_Adq_Actualizado"
        '
        'Precio_Venta_Fiscal
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N2"
        DataGridViewCellStyle12.NullValue = "0"
        Me.Precio_Venta_Fiscal.DefaultCellStyle = DataGridViewCellStyle12
        Me.Precio_Venta_Fiscal.HeaderText = "Precio Venta Fiscal"
        Me.Precio_Venta_Fiscal.Name = "Precio_Venta_Fiscal"
        '
        'Utilidad_Fiscal
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N2"
        DataGridViewCellStyle13.NullValue = "0"
        Me.Utilidad_Fiscal.DefaultCellStyle = DataGridViewCellStyle13
        Me.Utilidad_Fiscal.HeaderText = "Utilidad Fiscal"
        Me.Utilidad_Fiscal.Name = "Utilidad_Fiscal"
        '
        'Perdida_Fiscal
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Format = "N2"
        DataGridViewCellStyle14.NullValue = "0"
        Me.Perdida_Fiscal.DefaultCellStyle = DataGridViewCellStyle14
        Me.Perdida_Fiscal.HeaderText = "Perdida Fiscal"
        Me.Perdida_Fiscal.Name = "Perdida_Fiscal"
        '
        'TipoPoliza
        '
        Me.TipoPoliza.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.TipoPoliza.HeaderText = "Tipo de Poliza"
        Me.TipoPoliza.Name = "TipoPoliza"
        Me.TipoPoliza.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TipoPoliza.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Numero
        '
        Me.Numero.HeaderText = "Numero de Poliza"
        Me.Numero.Name = "Numero"
        '
        'Año
        '
        Me.Año.HeaderText = "Año Contable"
        Me.Año.Name = "Año"
        '
        'Mes
        '
        Me.Mes.HeaderText = "Mes Contable"
        Me.Mes.Name = "Mes"
        '
        'Tipo
        '
        Me.Tipo.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.Tipo.HeaderText = "Tipo"
        Me.Tipo.Name = "Tipo"
        Me.Tipo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tipo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Bo
        '
        Me.Bo.HeaderText = "Banco Origen"
        Me.Bo.Name = "Bo"
        '
        'CtaOrigen
        '
        Me.CtaOrigen.HeaderText = "Cuenta Origen"
        Me.CtaOrigen.Name = "CtaOrigen"
        '
        'BD
        '
        Me.BD.HeaderText = "Banco Destino"
        Me.BD.Name = "BD"
        '
        'CtaDestino
        '
        Me.CtaDestino.HeaderText = "Cuenta Destino"
        Me.CtaDestino.Name = "CtaDestino"
        '
        'FechaMov
        '
        Me.FechaMov.HeaderText = "Fecha Movimiento"
        Me.FechaMov.Name = "FechaMov"
        '
        'Nocheque
        '
        Me.Nocheque.HeaderText = "Numero de Cheque"
        Me.Nocheque.Name = "Nocheque"
        '
        'VentadeTerrenos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(835, 272)
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VentadeTerrenos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Venta de Terrenos"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.LstAnio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdPoliza, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdCalcular, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdAgregar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents Ayuda As ToolTip
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdAgregar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdPoliza As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdCalcular As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdGuardar As Telerik.WinControls.UI.RadButton
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents LstAnio As Telerik.WinControls.UI.RadDropDownList
	Friend WithEvents Label1 As Label
	Friend WithEvents TablaImportar As DataGridView
	Friend WithEvents Aplicar As DataGridViewCheckBoxColumn
	Friend WithEvents UUID As DataGridViewTextBoxColumn
	Friend WithEvents Cliente As DataGridViewTextBoxColumn
	Friend WithEvents Fecha_Adquisicion As DataGridViewTextBoxColumn
	Friend WithEvents Precio_Adquisicion As DataGridViewTextBoxColumn
	Friend WithEvents Referencia As DataGridViewTextBoxColumn
	Friend WithEvents Fecha_de_Venta As DataGridViewTextBoxColumn
	Friend WithEvents Precio_Venta As DataGridViewTextBoxColumn
	Friend WithEvents CtaPago As DataGridViewTextBoxColumn
	Friend WithEvents Utilidad_Contable As DataGridViewTextBoxColumn
	Friend WithEvents CtaUtilidad As DataGridViewTextBoxColumn
	Friend WithEvents Perdida_Contable As DataGridViewTextBoxColumn
	Friend WithEvents CtaPerdida As DataGridViewTextBoxColumn
	Friend WithEvents UltimoMes As DataGridViewTextBoxColumn
	Friend WithEvents ImporteUltimomes As DataGridViewTextBoxColumn
	Friend WithEvents Fecha_Adq As DataGridViewTextBoxColumn
	Friend WithEvents INPC_Adquisicion As DataGridViewTextBoxColumn
	Friend WithEvents Factor_Actualizacion As DataGridViewTextBoxColumn
	Friend WithEvents Precio_Adq_Actualizado As DataGridViewTextBoxColumn
	Friend WithEvents Precio_Venta_Fiscal As DataGridViewTextBoxColumn
	Friend WithEvents Utilidad_Fiscal As DataGridViewTextBoxColumn
	Friend WithEvents Perdida_Fiscal As DataGridViewTextBoxColumn
	Friend WithEvents TipoPoliza As DataGridViewComboBoxColumn
	Friend WithEvents Numero As DataGridViewTextBoxColumn
	Friend WithEvents Año As DataGridViewTextBoxColumn
	Friend WithEvents Mes As DataGridViewTextBoxColumn
	Friend WithEvents Tipo As DataGridViewComboBoxColumn
	Friend WithEvents Bo As DataGridViewTextBoxColumn
	Friend WithEvents CtaOrigen As DataGridViewTextBoxColumn
	Friend WithEvents BD As DataGridViewTextBoxColumn
	Friend WithEvents CtaDestino As DataGridViewTextBoxColumn
	Friend WithEvents FechaMov As DataGridViewTextBoxColumn
	Friend WithEvents Nocheque As DataGridViewTextBoxColumn
End Class

