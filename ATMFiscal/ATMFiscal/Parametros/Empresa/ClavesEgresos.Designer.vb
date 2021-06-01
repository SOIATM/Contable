<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ClavesEgresos
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ClavesEgresos))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.LblFacturasPPD = New System.Windows.Forms.Label()
        Me.CmdBuscarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevoF = New Telerik.WinControls.UI.RadButton()
        Me.CmdExportarf = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.SP = New System.ComponentModel.BackgroundWorker()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Guardar = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Eliminar = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Negativo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Clave = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tasa = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ligar = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.GravadoPUE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelG = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoG = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ExentoPUE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoE = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IVAPUE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoI = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RISRPUE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelRISR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoRISR = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RIVAPUE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelRIVA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoRIVA = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.OtraRPUE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelOtraR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoOtraR = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Espacio1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GravadoPPD = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelGP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoGP = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ExentoPPD = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelEP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoEP = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IVAPPD = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelIP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoIP = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RISRPPD = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelRISRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoRISRP = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RIVAPPD = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelRIVAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoRIVAP = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IVAPA = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelIVAPA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoIVAPA = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.OtraRPPD = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelOtraRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoOtraRP = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Debe = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoD = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DebeE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelDE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoDE = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.es = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnticipoGPUE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoA = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AnticipoEPUE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelAE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoAE = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IVAAPUE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelIA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoIA = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AnticipoGPPD = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoAP = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AnticipoEPPD = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelAEP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoAEP = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IVAAPPD = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelIAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoIAP = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.e3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COGC = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelCOGC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoCOGC = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.COGA = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelCOGA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoCOGA = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.COEC = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelCOEC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoCOEC = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.COEA = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NivelCOEA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CargoCOEA = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdExportarf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Controls.Add(Me.LblFacturasPPD)
        Me.RadPanel1.Controls.Add(Me.CmdBuscarF)
        Me.RadPanel1.Controls.Add(Me.CmdNuevoF)
        Me.RadPanel1.Controls.Add(Me.CmdExportarf)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label20)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1430, 91)
        Me.RadPanel1.TabIndex = 0
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(299, 9)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(175, 67)
        Me.CmdLimpiar.TabIndex = 690
        Me.CmdLimpiar.Text = "Limpiar Dato"
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'LblFacturasPPD
        '
        Me.LblFacturasPPD.AutoSize = True
        Me.LblFacturasPPD.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFacturasPPD.Location = New System.Drawing.Point(1064, 50)
        Me.LblFacturasPPD.Name = "LblFacturasPPD"
        Me.LblFacturasPPD.Size = New System.Drawing.Size(0, 18)
        Me.LblFacturasPPD.TabIndex = 689
        '
        'CmdBuscarF
        '
        Me.CmdBuscarF.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!)
        Me.CmdBuscarF.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarF.Location = New System.Drawing.Point(80, 12)
        Me.CmdBuscarF.Name = "CmdBuscarF"
        Me.CmdBuscarF.Size = New System.Drawing.Size(67, 67)
        Me.CmdBuscarF.TabIndex = 94
        Me.CmdBuscarF.TabStop = False
        Me.CmdBuscarF.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdBuscarF.ThemeName = "Aqua"
        '
        'CmdNuevoF
        '
        Me.CmdNuevoF.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!)
        Me.CmdNuevoF.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevoF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevoF.Location = New System.Drawing.Point(153, 12)
        Me.CmdNuevoF.Name = "CmdNuevoF"
        Me.CmdNuevoF.Size = New System.Drawing.Size(67, 67)
        Me.CmdNuevoF.TabIndex = 92
        Me.CmdNuevoF.TabStop = False
        Me.CmdNuevoF.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdNuevoF.ThemeName = "Aqua"
        '
        'CmdExportarf
        '
        Me.CmdExportarf.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!)
        Me.CmdExportarf.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.CmdExportarf.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdExportarf.Location = New System.Drawing.Point(226, 12)
        Me.CmdExportarf.Name = "CmdExportarf"
        Me.CmdExportarf.Size = New System.Drawing.Size(67, 67)
        Me.CmdExportarf.TabIndex = 93
        Me.CmdExportarf.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdExportarf.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCerrar.Image = CType(resources.GetObject("cmdCerrar.Image"), System.Drawing.Image)
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(7, 12)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(67, 67)
        Me.cmdCerrar.TabIndex = 91
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(500, 40)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(553, 36)
        Me.lstCliente.TabIndex = 90
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label20.Location = New System.Drawing.Point(497, 14)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(72, 18)
        Me.Label20.TabIndex = 89
        Me.Label20.Text = "Empresa:"
        '
        'SP
        '
        Me.SP.WorkerSupportsCancellation = True
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.Tabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.Guardar, Me.Eliminar, Me.Negativo, Me.Nombre, Me.Descripcion, Me.Clave, Me.Tasa, Me.Ligar, Me.GravadoPUE, Me.NivelG, Me.CargoG, Me.ExentoPUE, Me.NivelE, Me.CargoE, Me.IVAPUE, Me.NivelI, Me.CargoI, Me.RISRPUE, Me.NivelRISR, Me.CargoRISR, Me.RIVAPUE, Me.NivelRIVA, Me.CargoRIVA, Me.OtraRPUE, Me.NivelOtraR, Me.CargoOtraR, Me.Espacio1, Me.GravadoPPD, Me.NivelGP, Me.CargoGP, Me.ExentoPPD, Me.NivelEP, Me.CargoEP, Me.IVAPPD, Me.NivelIP, Me.CargoIP, Me.RISRPPD, Me.NivelRISRP, Me.CargoRISRP, Me.RIVAPPD, Me.NivelRIVAP, Me.CargoRIVAP, Me.IVAPA, Me.NivelIVAPA, Me.CargoIVAPA, Me.OtraRPPD, Me.NivelOtraRP, Me.CargoOtraRP, Me.Debe, Me.NivelD, Me.CargoD, Me.DebeE, Me.NivelDE, Me.CargoDE, Me.es, Me.AnticipoGPUE, Me.NivelA, Me.CargoA, Me.AnticipoEPUE, Me.NivelAE, Me.CargoAE, Me.IVAAPUE, Me.NivelIA, Me.CargoIA, Me.AnticipoGPPD, Me.NivelAP, Me.CargoAP, Me.AnticipoEPPD, Me.NivelAEP, Me.CargoAEP, Me.IVAAPPD, Me.NivelIAP, Me.CargoIAP, Me.e3, Me.COGC, Me.NivelCOGC, Me.CargoCOGC, Me.COGA, Me.NivelCOGA, Me.CargoCOGA, Me.COEC, Me.NivelCOEC, Me.CargoCOEC, Me.COEA, Me.NivelCOEA, Me.CargoCOEA})
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 91)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.RowTemplate.Height = 24
        Me.Tabla.Size = New System.Drawing.Size(1430, 489)
        Me.Tabla.TabIndex = 9
        '
        'Id
        '
        Me.Id.Frozen = True
        Me.Id.HeaderText = "id"
        Me.Id.Name = "Id"
        Me.Id.Visible = False
        Me.Id.Width = 48
        '
        'Guardar
        '
        Me.Guardar.Frozen = True
        Me.Guardar.HeaderText = "Guardar"
        Me.Guardar.Name = "Guardar"
        Me.Guardar.Width = 65
        '
        'Eliminar
        '
        Me.Eliminar.Frozen = True
        Me.Eliminar.HeaderText = "Eliminar"
        Me.Eliminar.Name = "Eliminar"
        Me.Eliminar.Width = 63
        '
        'Negativo
        '
        Me.Negativo.Frozen = True
        Me.Negativo.HeaderText = "Importe Negativo"
        Me.Negativo.Name = "Negativo"
        Me.Negativo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Negativo.Width = 104
        '
        'Nombre
        '
        Me.Nombre.Frozen = True
        Me.Nombre.HeaderText = "Nombre de la Poliza"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.Width = 112
        '
        'Descripcion
        '
        Me.Descripcion.Frozen = True
        Me.Descripcion.HeaderText = "Descripcion"
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.Width = 107
        '
        'Clave
        '
        Me.Clave.Frozen = True
        Me.Clave.HeaderText = "Clave"
        Me.Clave.Name = "Clave"
        Me.Clave.Width = 69
        '
        'Tasa
        '
        Me.Tasa.Frozen = True
        Me.Tasa.HeaderText = "Tasa"
        Me.Tasa.Name = "Tasa"
        Me.Tasa.Width = 65
        '
        'Ligar
        '
        Me.Ligar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Ligar.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.Ligar.HeaderText = "Clave Can. Provision"
        Me.Ligar.Name = "Ligar"
        Me.Ligar.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Ligar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Ligar.Width = 144
        '
        'GravadoPUE
        '
        Me.GravadoPUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.GravadoPUE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.GravadoPUE.HeaderText = "Gravado PUE"
        Me.GravadoPUE.MaxDropDownItems = 10
        Me.GravadoPUE.Name = "GravadoPUE"
        Me.GravadoPUE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GravadoPUE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.GravadoPUE.Width = 107
        '
        'NivelG
        '
        Me.NivelG.HeaderText = "Nivel"
        Me.NivelG.Name = "NivelG"
        Me.NivelG.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelG.Width = 65
        '
        'CargoG
        '
        Me.CargoG.HeaderText = "Cargo"
        Me.CargoG.Name = "CargoG"
        Me.CargoG.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoG.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CargoG.Width = 72
        '
        'ExentoPUE
        '
        Me.ExentoPUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.ExentoPUE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.ExentoPUE.HeaderText = "Exento PUE"
        Me.ExentoPUE.Name = "ExentoPUE"
        Me.ExentoPUE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ExentoPUE.Width = 73
        '
        'NivelE
        '
        Me.NivelE.HeaderText = "Nivel"
        Me.NivelE.Name = "NivelE"
        Me.NivelE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NivelE.Width = 42
        '
        'CargoE
        '
        Me.CargoE.HeaderText = "Cargo"
        Me.CargoE.Name = "CargoE"
        Me.CargoE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoE.Width = 49
        '
        'IVAPUE
        '
        Me.IVAPUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.IVAPUE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.IVAPUE.HeaderText = "IVA PUE"
        Me.IVAPUE.Name = "IVAPUE"
        Me.IVAPUE.Width = 56
        '
        'NivelI
        '
        Me.NivelI.HeaderText = "Nivel"
        Me.NivelI.Name = "NivelI"
        Me.NivelI.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NivelI.Width = 42
        '
        'CargoI
        '
        Me.CargoI.HeaderText = "Cargo"
        Me.CargoI.Name = "CargoI"
        Me.CargoI.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoI.Width = 49
        '
        'RISRPUE
        '
        Me.RISRPUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.RISRPUE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.RISRPUE.HeaderText = "RISR PUE"
        Me.RISRPUE.Name = "RISRPUE"
        Me.RISRPUE.Width = 64
        '
        'NivelRISR
        '
        Me.NivelRISR.HeaderText = "Nivel"
        Me.NivelRISR.Name = "NivelRISR"
        Me.NivelRISR.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelRISR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NivelRISR.Width = 42
        '
        'CargoRISR
        '
        Me.CargoRISR.HeaderText = "Cargo"
        Me.CargoRISR.Name = "CargoRISR"
        Me.CargoRISR.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoRISR.Width = 49
        '
        'RIVAPUE
        '
        Me.RIVAPUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.RIVAPUE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.RIVAPUE.HeaderText = "RIVA PUE"
        Me.RIVAPUE.Name = "RIVAPUE"
        Me.RIVAPUE.Width = 64
        '
        'NivelRIVA
        '
        Me.NivelRIVA.HeaderText = "Nivel"
        Me.NivelRIVA.Name = "NivelRIVA"
        Me.NivelRIVA.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelRIVA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NivelRIVA.Width = 42
        '
        'CargoRIVA
        '
        Me.CargoRIVA.HeaderText = "Cargo"
        Me.CargoRIVA.Name = "CargoRIVA"
        Me.CargoRIVA.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoRIVA.Width = 49
        '
        'OtraRPUE
        '
        Me.OtraRPUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.OtraRPUE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.OtraRPUE.HeaderText = "Otra Retención PUE"
        Me.OtraRPUE.Name = "OtraRPUE"
        Me.OtraRPUE.Width = 119
        '
        'NivelOtraR
        '
        Me.NivelOtraR.HeaderText = "Nivel"
        Me.NivelOtraR.Name = "NivelOtraR"
        Me.NivelOtraR.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelOtraR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NivelOtraR.Width = 42
        '
        'CargoOtraR
        '
        Me.CargoOtraR.HeaderText = "Cargo"
        Me.CargoOtraR.Name = "CargoOtraR"
        Me.CargoOtraR.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoOtraR.Width = 49
        '
        'Espacio1
        '
        Me.Espacio1.HeaderText = ""
        Me.Espacio1.Name = "Espacio1"
        Me.Espacio1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Espacio1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Espacio1.Width = 5
        '
        'GravadoPPD
        '
        Me.GravadoPPD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.GravadoPPD.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.GravadoPPD.HeaderText = "Gravado PPD"
        Me.GravadoPPD.Name = "GravadoPPD"
        Me.GravadoPPD.Width = 85
        '
        'NivelGP
        '
        Me.NivelGP.HeaderText = "Nivel"
        Me.NivelGP.Name = "NivelGP"
        Me.NivelGP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelGP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NivelGP.Width = 42
        '
        'CargoGP
        '
        Me.CargoGP.HeaderText = "Cargo"
        Me.CargoGP.Name = "CargoGP"
        Me.CargoGP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoGP.Width = 49
        '
        'ExentoPPD
        '
        Me.ExentoPPD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.ExentoPPD.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.ExentoPPD.HeaderText = "Exento PPD"
        Me.ExentoPPD.Name = "ExentoPPD"
        Me.ExentoPPD.Width = 74
        '
        'NivelEP
        '
        Me.NivelEP.HeaderText = "Nivel"
        Me.NivelEP.Name = "NivelEP"
        Me.NivelEP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelEP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NivelEP.Width = 42
        '
        'CargoEP
        '
        Me.CargoEP.HeaderText = "Cargo"
        Me.CargoEP.Name = "CargoEP"
        Me.CargoEP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoEP.Width = 49
        '
        'IVAPPD
        '
        Me.IVAPPD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.IVAPPD.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.IVAPPD.HeaderText = "IVA PPD"
        Me.IVAPPD.Name = "IVAPPD"
        Me.IVAPPD.Width = 57
        '
        'NivelIP
        '
        Me.NivelIP.HeaderText = "Nivel"
        Me.NivelIP.Name = "NivelIP"
        Me.NivelIP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NivelIP.Width = 42
        '
        'CargoIP
        '
        Me.CargoIP.HeaderText = "Cargo"
        Me.CargoIP.Name = "CargoIP"
        Me.CargoIP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoIP.Width = 49
        '
        'RISRPPD
        '
        Me.RISRPPD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.RISRPPD.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.RISRPPD.HeaderText = "RISR PPD"
        Me.RISRPPD.Name = "RISRPPD"
        Me.RISRPPD.Width = 65
        '
        'NivelRISRP
        '
        Me.NivelRISRP.HeaderText = "Nivel"
        Me.NivelRISRP.Name = "NivelRISRP"
        Me.NivelRISRP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelRISRP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NivelRISRP.Width = 42
        '
        'CargoRISRP
        '
        Me.CargoRISRP.HeaderText = "Cargo"
        Me.CargoRISRP.Name = "CargoRISRP"
        Me.CargoRISRP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoRISRP.Width = 49
        '
        'RIVAPPD
        '
        Me.RIVAPPD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.RIVAPPD.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.RIVAPPD.HeaderText = "RIVA PPD"
        Me.RIVAPPD.Name = "RIVAPPD"
        Me.RIVAPPD.Width = 65
        '
        'NivelRIVAP
        '
        Me.NivelRIVAP.HeaderText = "Nivel"
        Me.NivelRIVAP.Name = "NivelRIVAP"
        Me.NivelRIVAP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelRIVAP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NivelRIVAP.Width = 42
        '
        'CargoRIVAP
        '
        Me.CargoRIVAP.HeaderText = "Cargo"
        Me.CargoRIVAP.Name = "CargoRIVAP"
        Me.CargoRIVAP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoRIVAP.Width = 49
        '
        'IVAPA
        '
        Me.IVAPA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.IVAPA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.IVAPA.HeaderText = "IVA Por Acreditar Retenido"
        Me.IVAPA.Name = "IVAPA"
        Me.IVAPA.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IVAPA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.IVAPA.Width = 180
        '
        'NivelIVAPA
        '
        Me.NivelIVAPA.HeaderText = "Nivel "
        Me.NivelIVAPA.Name = "NivelIVAPA"
        Me.NivelIVAPA.Width = 69
        '
        'CargoIVAPA
        '
        Me.CargoIVAPA.HeaderText = "Cargo"
        Me.CargoIVAPA.Name = "CargoIVAPA"
        Me.CargoIVAPA.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoIVAPA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CargoIVAPA.Width = 72
        '
        'OtraRPPD
        '
        Me.OtraRPPD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.OtraRPPD.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.OtraRPPD.HeaderText = "Otra Retención PPD"
        Me.OtraRPPD.Name = "OtraRPPD"
        Me.OtraRPPD.Width = 120
        '
        'NivelOtraRP
        '
        Me.NivelOtraRP.HeaderText = "Nivel"
        Me.NivelOtraRP.Name = "NivelOtraRP"
        Me.NivelOtraRP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NivelOtraRP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NivelOtraRP.Width = 42
        '
        'CargoOtraRP
        '
        Me.CargoOtraRP.HeaderText = "Cargo"
        Me.CargoOtraRP.Name = "CargoOtraRP"
        Me.CargoOtraRP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoOtraRP.Width = 49
        '
        'Debe
        '
        Me.Debe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Debe.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.Debe.HeaderText = "A quien se Debe Gravado"
        Me.Debe.Name = "Debe"
        Me.Debe.Width = 104
        '
        'NivelD
        '
        Me.NivelD.HeaderText = "Nivel"
        Me.NivelD.Name = "NivelD"
        Me.NivelD.Width = 65
        '
        'CargoD
        '
        Me.CargoD.HeaderText = "Cargo"
        Me.CargoD.Name = "CargoD"
        Me.CargoD.Width = 49
        '
        'DebeE
        '
        Me.DebeE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DebeE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.DebeE.HeaderText = "A quien se Debe Exento"
        Me.DebeE.Name = "DebeE"
        Me.DebeE.Width = 104
        '
        'NivelDE
        '
        Me.NivelDE.HeaderText = "Nivel"
        Me.NivelDE.Name = "NivelDE"
        Me.NivelDE.Width = 65
        '
        'CargoDE
        '
        Me.CargoDE.HeaderText = "Cargo"
        Me.CargoDE.Name = "CargoDE"
        Me.CargoDE.Width = 49
        '
        'es
        '
        Me.es.HeaderText = ""
        Me.es.Name = "es"
        Me.es.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.es.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.es.Width = 5
        '
        'AnticipoGPUE
        '
        Me.AnticipoGPUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.AnticipoGPUE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.AnticipoGPUE.HeaderText = "Anticipo Gravado PUE"
        Me.AnticipoGPUE.Name = "AnticipoGPUE"
        Me.AnticipoGPUE.Width = 109
        '
        'NivelA
        '
        Me.NivelA.HeaderText = "Nivel"
        Me.NivelA.Name = "NivelA"
        Me.NivelA.Width = 65
        '
        'CargoA
        '
        Me.CargoA.HeaderText = "Cargo"
        Me.CargoA.Name = "CargoA"
        Me.CargoA.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CargoA.Width = 72
        '
        'AnticipoEPUE
        '
        Me.AnticipoEPUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.AnticipoEPUE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.AnticipoEPUE.HeaderText = "Anticipo Exento PUE"
        Me.AnticipoEPUE.Name = "AnticipoEPUE"
        Me.AnticipoEPUE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnticipoEPUE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.AnticipoEPUE.Width = 121
        '
        'NivelAE
        '
        Me.NivelAE.HeaderText = "Nivel"
        Me.NivelAE.Name = "NivelAE"
        Me.NivelAE.Width = 65
        '
        'CargoAE
        '
        Me.CargoAE.HeaderText = "Cargo"
        Me.CargoAE.Name = "CargoAE"
        Me.CargoAE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoAE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CargoAE.Width = 72
        '
        'IVAAPUE
        '
        Me.IVAAPUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.IVAAPUE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.IVAAPUE.HeaderText = "IVA Anticipo PUE"
        Me.IVAAPUE.Name = "IVAAPUE"
        Me.IVAAPUE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IVAAPUE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.IVAAPUE.Width = 126
        '
        'NivelIA
        '
        Me.NivelIA.HeaderText = "Nivel"
        Me.NivelIA.Name = "NivelIA"
        Me.NivelIA.Width = 65
        '
        'CargoIA
        '
        Me.CargoIA.HeaderText = "Cargo"
        Me.CargoIA.Name = "CargoIA"
        Me.CargoIA.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoIA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CargoIA.Width = 72
        '
        'AnticipoGPPD
        '
        Me.AnticipoGPPD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.AnticipoGPPD.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.AnticipoGPPD.HeaderText = "Anticipo Gravado PPD"
        Me.AnticipoGPPD.Name = "AnticipoGPPD"
        Me.AnticipoGPPD.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnticipoGPPD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.AnticipoGPPD.Width = 132
        '
        'NivelAP
        '
        Me.NivelAP.HeaderText = "Nivel"
        Me.NivelAP.Name = "NivelAP"
        Me.NivelAP.Width = 65
        '
        'CargoAP
        '
        Me.CargoAP.HeaderText = "Cargo"
        Me.CargoAP.Name = "CargoAP"
        Me.CargoAP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoAP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CargoAP.Width = 72
        '
        'AnticipoEPPD
        '
        Me.AnticipoEPPD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.AnticipoEPPD.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.AnticipoEPPD.HeaderText = "Anticipo Exento PPD"
        Me.AnticipoEPPD.Name = "AnticipoEPPD"
        Me.AnticipoEPPD.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnticipoEPPD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.AnticipoEPPD.Width = 121
        '
        'NivelAEP
        '
        Me.NivelAEP.HeaderText = "Nivel"
        Me.NivelAEP.Name = "NivelAEP"
        Me.NivelAEP.Width = 65
        '
        'CargoAEP
        '
        Me.CargoAEP.HeaderText = "Cargo"
        Me.CargoAEP.Name = "CargoAEP"
        Me.CargoAEP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoAEP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CargoAEP.Width = 72
        '
        'IVAAPPD
        '
        Me.IVAAPPD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.IVAAPPD.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.IVAAPPD.HeaderText = "IVA Anticipos PPD"
        Me.IVAAPPD.Name = "IVAAPPD"
        Me.IVAAPPD.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IVAAPPD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.IVAAPPD.Width = 132
        '
        'NivelIAP
        '
        Me.NivelIAP.HeaderText = "Nivel"
        Me.NivelIAP.Name = "NivelIAP"
        Me.NivelIAP.Width = 65
        '
        'CargoIAP
        '
        Me.CargoIAP.HeaderText = "Cargo"
        Me.CargoIAP.Name = "CargoIAP"
        Me.CargoIAP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CargoIAP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CargoIAP.Width = 72
        '
        'e3
        '
        Me.e3.HeaderText = ""
        Me.e3.Name = "e3"
        Me.e3.Width = 23
        '
        'COGC
        '
        Me.COGC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.COGC.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.COGC.HeaderText = "CO Gravados Cargados"
        Me.COGC.Name = "COGC"
        Me.COGC.Width = 138
        '
        'NivelCOGC
        '
        Me.NivelCOGC.HeaderText = "Nivel "
        Me.NivelCOGC.Name = "NivelCOGC"
        Me.NivelCOGC.Width = 69
        '
        'CargoCOGC
        '
        Me.CargoCOGC.HeaderText = "Cargo "
        Me.CargoCOGC.Name = "CargoCOGC"
        Me.CargoCOGC.Width = 53
        '
        'COGA
        '
        Me.COGA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.COGA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.COGA.HeaderText = "CO Gravados Abonados"
        Me.COGA.Name = "COGA"
        Me.COGA.Width = 140
        '
        'NivelCOGA
        '
        Me.NivelCOGA.HeaderText = "Nivel "
        Me.NivelCOGA.Name = "NivelCOGA"
        Me.NivelCOGA.Width = 69
        '
        'CargoCOGA
        '
        Me.CargoCOGA.HeaderText = "Cargo "
        Me.CargoCOGA.Name = "CargoCOGA"
        Me.CargoCOGA.Width = 53
        '
        'COEC
        '
        Me.COEC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.COEC.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.COEC.HeaderText = "CO Exentos Cargado"
        Me.COEC.Name = "COEC"
        Me.COEC.Width = 122
        '
        'NivelCOEC
        '
        Me.NivelCOEC.HeaderText = "Nivel"
        Me.NivelCOEC.Name = "NivelCOEC"
        Me.NivelCOEC.Width = 65
        '
        'CargoCOEC
        '
        Me.CargoCOEC.HeaderText = "Cargo"
        Me.CargoCOEC.Name = "CargoCOEC"
        Me.CargoCOEC.Width = 49
        '
        'COEA
        '
        Me.COEA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.COEA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.COEA.HeaderText = "CO Exento Abono"
        Me.COEA.Name = "COEA"
        Me.COEA.Width = 104
        '
        'NivelCOEA
        '
        Me.NivelCOEA.HeaderText = "Nivel"
        Me.NivelCOEA.Name = "NivelCOEA"
        Me.NivelCOEA.Width = 65
        '
        'CargoCOEA
        '
        Me.CargoCOEA.HeaderText = "Cargo "
        Me.CargoCOEA.Name = "CargoCOEA"
        Me.CargoCOEA.Width = 53
        '
        'ClavesEgresos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1430, 580)
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ClavesEgresos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Claves para Control de Gastos Generales"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdExportarf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label20 As Label
    Friend WithEvents CmdBuscarF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdNuevoF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdExportarf As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents SP As System.ComponentModel.BackgroundWorker
    Friend WithEvents LblFacturasPPD As Label
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents Tabla As DataGridView
    Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
    Friend WithEvents Id As DataGridViewTextBoxColumn
    Friend WithEvents Guardar As DataGridViewButtonColumn
    Friend WithEvents Eliminar As DataGridViewButtonColumn
    Friend WithEvents Negativo As DataGridViewCheckBoxColumn
    Friend WithEvents Nombre As DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As DataGridViewTextBoxColumn
    Friend WithEvents Clave As DataGridViewTextBoxColumn
    Friend WithEvents Tasa As DataGridViewTextBoxColumn
    Friend WithEvents Ligar As DataGridViewComboBoxColumn
    Friend WithEvents GravadoPUE As DataGridViewComboBoxColumn
    Friend WithEvents NivelG As DataGridViewTextBoxColumn
    Friend WithEvents CargoG As DataGridViewCheckBoxColumn
    Friend WithEvents ExentoPUE As DataGridViewComboBoxColumn
    Friend WithEvents NivelE As DataGridViewTextBoxColumn
    Friend WithEvents CargoE As DataGridViewCheckBoxColumn
    Friend WithEvents IVAPUE As DataGridViewComboBoxColumn
    Friend WithEvents NivelI As DataGridViewTextBoxColumn
    Friend WithEvents CargoI As DataGridViewCheckBoxColumn
    Friend WithEvents RISRPUE As DataGridViewComboBoxColumn
    Friend WithEvents NivelRISR As DataGridViewTextBoxColumn
    Friend WithEvents CargoRISR As DataGridViewCheckBoxColumn
    Friend WithEvents RIVAPUE As DataGridViewComboBoxColumn
    Friend WithEvents NivelRIVA As DataGridViewTextBoxColumn
    Friend WithEvents CargoRIVA As DataGridViewCheckBoxColumn
    Friend WithEvents OtraRPUE As DataGridViewComboBoxColumn
    Friend WithEvents NivelOtraR As DataGridViewTextBoxColumn
    Friend WithEvents CargoOtraR As DataGridViewCheckBoxColumn
    Friend WithEvents Espacio1 As DataGridViewTextBoxColumn
    Friend WithEvents GravadoPPD As DataGridViewComboBoxColumn
    Friend WithEvents NivelGP As DataGridViewTextBoxColumn
    Friend WithEvents CargoGP As DataGridViewCheckBoxColumn
    Friend WithEvents ExentoPPD As DataGridViewComboBoxColumn
    Friend WithEvents NivelEP As DataGridViewTextBoxColumn
    Friend WithEvents CargoEP As DataGridViewCheckBoxColumn
    Friend WithEvents IVAPPD As DataGridViewComboBoxColumn
    Friend WithEvents NivelIP As DataGridViewTextBoxColumn
    Friend WithEvents CargoIP As DataGridViewCheckBoxColumn
    Friend WithEvents RISRPPD As DataGridViewComboBoxColumn
    Friend WithEvents NivelRISRP As DataGridViewTextBoxColumn
    Friend WithEvents CargoRISRP As DataGridViewCheckBoxColumn
    Friend WithEvents RIVAPPD As DataGridViewComboBoxColumn
    Friend WithEvents NivelRIVAP As DataGridViewTextBoxColumn
    Friend WithEvents CargoRIVAP As DataGridViewCheckBoxColumn
    Friend WithEvents IVAPA As DataGridViewComboBoxColumn
    Friend WithEvents NivelIVAPA As DataGridViewTextBoxColumn
    Friend WithEvents CargoIVAPA As DataGridViewCheckBoxColumn
    Friend WithEvents OtraRPPD As DataGridViewComboBoxColumn
    Friend WithEvents NivelOtraRP As DataGridViewTextBoxColumn
    Friend WithEvents CargoOtraRP As DataGridViewCheckBoxColumn
    Friend WithEvents Debe As DataGridViewComboBoxColumn
    Friend WithEvents NivelD As DataGridViewTextBoxColumn
    Friend WithEvents CargoD As DataGridViewCheckBoxColumn
    Friend WithEvents DebeE As DataGridViewComboBoxColumn
    Friend WithEvents NivelDE As DataGridViewTextBoxColumn
    Friend WithEvents CargoDE As DataGridViewCheckBoxColumn
    Friend WithEvents es As DataGridViewTextBoxColumn
    Friend WithEvents AnticipoGPUE As DataGridViewComboBoxColumn
    Friend WithEvents NivelA As DataGridViewTextBoxColumn
    Friend WithEvents CargoA As DataGridViewCheckBoxColumn
    Friend WithEvents AnticipoEPUE As DataGridViewComboBoxColumn
    Friend WithEvents NivelAE As DataGridViewTextBoxColumn
    Friend WithEvents CargoAE As DataGridViewCheckBoxColumn
    Friend WithEvents IVAAPUE As DataGridViewComboBoxColumn
    Friend WithEvents NivelIA As DataGridViewTextBoxColumn
    Friend WithEvents CargoIA As DataGridViewCheckBoxColumn
    Friend WithEvents AnticipoGPPD As DataGridViewComboBoxColumn
    Friend WithEvents NivelAP As DataGridViewTextBoxColumn
    Friend WithEvents CargoAP As DataGridViewCheckBoxColumn
    Friend WithEvents AnticipoEPPD As DataGridViewComboBoxColumn
    Friend WithEvents NivelAEP As DataGridViewTextBoxColumn
    Friend WithEvents CargoAEP As DataGridViewCheckBoxColumn
    Friend WithEvents IVAAPPD As DataGridViewComboBoxColumn
    Friend WithEvents NivelIAP As DataGridViewTextBoxColumn
    Friend WithEvents CargoIAP As DataGridViewCheckBoxColumn
    Friend WithEvents e3 As DataGridViewTextBoxColumn
    Friend WithEvents COGC As DataGridViewComboBoxColumn
    Friend WithEvents NivelCOGC As DataGridViewTextBoxColumn
    Friend WithEvents CargoCOGC As DataGridViewCheckBoxColumn
    Friend WithEvents COGA As DataGridViewComboBoxColumn
    Friend WithEvents NivelCOGA As DataGridViewTextBoxColumn
    Friend WithEvents CargoCOGA As DataGridViewCheckBoxColumn
    Friend WithEvents COEC As DataGridViewComboBoxColumn
    Friend WithEvents NivelCOEC As DataGridViewTextBoxColumn
    Friend WithEvents CargoCOEC As DataGridViewCheckBoxColumn
    Friend WithEvents COEA As DataGridViewComboBoxColumn
    Friend WithEvents NivelCOEA As DataGridViewTextBoxColumn
    Friend WithEvents CargoCOEA As DataGridViewCheckBoxColumn
End Class

