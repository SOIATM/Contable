<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClavesEgresosGastos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ClavesEgresosGastos))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.Barra = New Telerik.WinControls.UI.RadProgressBar()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CmdGuardarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevoF = New Telerik.WinControls.UI.RadButton()
        Me.CmdSalirF = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarFact = New Telerik.WinControls.UI.RadButton()
        Me.CmdEliminarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiarF = New Telerik.WinControls.UI.RadButton()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.TablaSeries = New System.Windows.Forms.DataGridView()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.SelFactura = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Egresos = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Clave = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tasa = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaEgG = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtaEgEx = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtaEgC = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IVAAcre = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtaPEgG = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtaPEgEx = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtaPEgC = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IVAPAcre = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Deudor = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Efectivo = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Transferencia = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Cheque = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Terceros = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.TipPol = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaSeries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel1.Controls.Add(Me.RadButton1)
        Me.RadPanel1.Controls.Add(Me.Barra)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label5)
        Me.RadPanel1.Controls.Add(Me.CmdGuardarF)
        Me.RadPanel1.Controls.Add(Me.CmdNuevoF)
        Me.RadPanel1.Controls.Add(Me.CmdSalirF)
        Me.RadPanel1.Controls.Add(Me.CmdBuscarFact)
        Me.RadPanel1.Controls.Add(Me.CmdEliminarF)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiarF)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1693, 124)
        Me.RadPanel1.TabIndex = 0
        Me.RadPanel1.ThemeName = "Material"
        '
        'RadButton1
        '
        Me.RadButton1.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.RadButton1.Location = New System.Drawing.Point(333, 10)
        Me.RadButton1.Margin = New System.Windows.Forms.Padding(2)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(147, 54)
        Me.RadButton1.TabIndex = 597
        Me.RadButton1.TabStop = False
        Me.RadButton1.Text = "Limpiar Cuenta"
        Me.RadButton1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.RadButton1.ThemeName = "Aqua"
        '
        'Barra
        '
        Me.Barra.Location = New System.Drawing.Point(9, 72)
        Me.Barra.Name = "Barra"
        Me.Barra.Size = New System.Drawing.Size(846, 36)
        Me.Barra.TabIndex = 596
        Me.Barra.Text = " "
        Me.Barra.ThemeName = "Material"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(482, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 18)
        Me.Label5.TabIndex = 588
        Me.Label5.Text = "Empresa:"
        '
        'CmdGuardarF
        '
        Me.CmdGuardarF.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardarF.Location = New System.Drawing.Point(279, 11)
        Me.CmdGuardarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardarF.Name = "CmdGuardarF"
        Me.CmdGuardarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardarF.TabIndex = 589
        Me.CmdGuardarF.TabStop = False
        Me.CmdGuardarF.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdGuardarF.ThemeName = "Aqua"
        '
        'CmdNuevoF
        '
        Me.CmdNuevoF.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevoF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevoF.Location = New System.Drawing.Point(171, 11)
        Me.CmdNuevoF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdNuevoF.Name = "CmdNuevoF"
        Me.CmdNuevoF.Size = New System.Drawing.Size(50, 54)
        Me.CmdNuevoF.TabIndex = 591
        Me.CmdNuevoF.TabStop = False
        Me.CmdNuevoF.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdNuevoF.ThemeName = "Aqua"
        '
        'CmdSalirF
        '
        Me.CmdSalirF.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.CmdSalirF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdSalirF.Location = New System.Drawing.Point(9, 11)
        Me.CmdSalirF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdSalirF.Name = "CmdSalirF"
        Me.CmdSalirF.Size = New System.Drawing.Size(50, 54)
        Me.CmdSalirF.TabIndex = 595
        Me.CmdSalirF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdSalirF.ThemeName = "Aqua"
        '
        'CmdBuscarFact
        '
        Me.CmdBuscarFact.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscarFact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarFact.Location = New System.Drawing.Point(117, 11)
        Me.CmdBuscarFact.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarFact.Name = "CmdBuscarFact"
        Me.CmdBuscarFact.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscarFact.TabIndex = 592
        Me.CmdBuscarFact.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdBuscarFact.ThemeName = "Aqua"
        '
        'CmdEliminarF
        '
        Me.CmdEliminarF.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminarF.Location = New System.Drawing.Point(225, 11)
        Me.CmdEliminarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdEliminarF.Name = "CmdEliminarF"
        Me.CmdEliminarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdEliminarF.TabIndex = 594
        Me.CmdEliminarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEliminarF.ThemeName = "Aqua"
        '
        'CmdLimpiarF
        '
        Me.CmdLimpiarF.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiarF.Location = New System.Drawing.Point(63, 13)
        Me.CmdLimpiarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiarF.Name = "CmdLimpiarF"
        Me.CmdLimpiarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiarF.TabIndex = 593
        Me.CmdLimpiarF.TabStop = False
        Me.CmdLimpiarF.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdLimpiarF.ThemeName = "Aqua"
        '
        'TablaSeries
        '
        Me.TablaSeries.AllowUserToAddRows = False
        Me.TablaSeries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaSeries.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SelFactura, Me.Egresos, Me.Clave, Me.Tasa, Me.CtaEgG, Me.CtaEgEx, Me.CtaEgC, Me.IVAAcre, Me.CtaPEgG, Me.CtaPEgEx, Me.CtaPEgC, Me.IVAPAcre, Me.Deudor, Me.Efectivo, Me.Transferencia, Me.Cheque, Me.Terceros, Me.TipPol, Me.ID})
        Me.TablaSeries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaSeries.Location = New System.Drawing.Point(0, 124)
        Me.TablaSeries.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TablaSeries.Name = "TablaSeries"
        Me.TablaSeries.RowTemplate.Height = 24
        Me.TablaSeries.Size = New System.Drawing.Size(1693, 341)
        Me.TablaSeries.TabIndex = 4
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(485, 28)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(370, 36)
        Me.lstCliente.TabIndex = 590
        '
        'SelFactura
        '
        Me.SelFactura.HeaderText = "Selecciona"
        Me.SelFactura.Name = "SelFactura"
        '
        'Egresos
        '
        Me.Egresos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Egresos.HeaderText = "Egreso"
        Me.Egresos.Name = "Egresos"
        Me.Egresos.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Egresos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Egresos.Width = 71
        '
        'Clave
        '
        Me.Clave.HeaderText = "Clave"
        Me.Clave.Name = "Clave"
        '
        'Tasa
        '
        Me.Tasa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Tasa.HeaderText = "Tasa"
        Me.Tasa.Name = "Tasa"
        Me.Tasa.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tasa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tasa.Width = 55
        '
        'CtaEgG
        '
        Me.CtaEgG.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaEgG.HeaderText = "Egreso Gravable"
        Me.CtaEgG.Name = "CtaEgG"
        Me.CtaEgG.Width = 300
        '
        'CtaEgEx
        '
        Me.CtaEgEx.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaEgEx.HeaderText = "Egreso Exento"
        Me.CtaEgEx.Name = "CtaEgEx"
        Me.CtaEgEx.Width = 300
        '
        'CtaEgC
        '
        Me.CtaEgC.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaEgC.HeaderText = "Egreso Cero"
        Me.CtaEgC.Name = "CtaEgC"
        Me.CtaEgC.Width = 300
        '
        'IVAAcre
        '
        Me.IVAAcre.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.IVAAcre.HeaderText = "IVA Acreditable"
        Me.IVAAcre.Name = "IVAAcre"
        Me.IVAAcre.Width = 300
        '
        'CtaPEgG
        '
        Me.CtaPEgG.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaPEgG.HeaderText = "Egreso Provisionado Gravado"
        Me.CtaPEgG.Name = "CtaPEgG"
        Me.CtaPEgG.Width = 300
        '
        'CtaPEgEx
        '
        Me.CtaPEgEx.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaPEgEx.HeaderText = "Egreso Provisionado Exento"
        Me.CtaPEgEx.Name = "CtaPEgEx"
        Me.CtaPEgEx.Width = 300
        '
        'CtaPEgC
        '
        Me.CtaPEgC.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaPEgC.HeaderText = "Egreso Provisionado Cero"
        Me.CtaPEgC.Name = "CtaPEgC"
        Me.CtaPEgC.Width = 300
        '
        'IVAPAcre
        '
        Me.IVAPAcre.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.IVAPAcre.HeaderText = "IVA Por Acreditar"
        Me.IVAPAcre.Name = "IVAPAcre"
        Me.IVAPAcre.Width = 300
        '
        'Deudor
        '
        Me.Deudor.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.Deudor.HeaderText = "Deudor"
        Me.Deudor.Name = "Deudor"
        Me.Deudor.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Deudor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Deudor.Width = 300
        '
        'Efectivo
        '
        Me.Efectivo.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.Efectivo.HeaderText = "Efectivo"
        Me.Efectivo.Name = "Efectivo"
        Me.Efectivo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Efectivo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Efectivo.Width = 300
        '
        'Transferencia
        '
        Me.Transferencia.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.Transferencia.HeaderText = "Transferencia"
        Me.Transferencia.Name = "Transferencia"
        Me.Transferencia.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Transferencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Transferencia.Width = 300
        '
        'Cheque
        '
        Me.Cheque.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.Cheque.HeaderText = "Cheque"
        Me.Cheque.Name = "Cheque"
        Me.Cheque.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Cheque.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Cheque.Width = 300
        '
        'Terceros
        '
        Me.Terceros.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.Terceros.HeaderText = "Terceros"
        Me.Terceros.Name = "Terceros"
        Me.Terceros.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Terceros.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Terceros.Width = 300
        '
        'TipPol
        '
        Me.TipPol.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.TipPol.HeaderText = "Tipo Poliza"
        Me.TipPol.Name = "TipPol"
        Me.TipPol.Width = 300
        '
        'ID
        '
        Me.ID.HeaderText = "id"
        Me.ID.Name = "ID"
        Me.ID.Visible = False
        '
        'ClavesEgresosGastos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1693, 465)
        Me.Controls.Add(Me.TablaSeries)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ClavesEgresosGastos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Claves Egresos para Gastos de Operación"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaSeries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label5 As Label
    Friend WithEvents CmdGuardarF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdNuevoF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdSalirF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdBuscarFact As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdEliminarF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdLimpiarF As Telerik.WinControls.UI.RadButton
    Friend WithEvents Barra As Telerik.WinControls.UI.RadProgressBar
    Friend WithEvents TablaSeries As DataGridView
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents SelFactura As DataGridViewCheckBoxColumn
    Friend WithEvents Egresos As DataGridViewTextBoxColumn
    Friend WithEvents Clave As DataGridViewTextBoxColumn
    Friend WithEvents Tasa As DataGridViewTextBoxColumn
    Friend WithEvents CtaEgG As DataGridViewComboBoxColumn
    Friend WithEvents CtaEgEx As DataGridViewComboBoxColumn
    Friend WithEvents CtaEgC As DataGridViewComboBoxColumn
    Friend WithEvents IVAAcre As DataGridViewComboBoxColumn
    Friend WithEvents CtaPEgG As DataGridViewComboBoxColumn
    Friend WithEvents CtaPEgEx As DataGridViewComboBoxColumn
    Friend WithEvents CtaPEgC As DataGridViewComboBoxColumn
    Friend WithEvents IVAPAcre As DataGridViewComboBoxColumn
    Friend WithEvents Deudor As DataGridViewComboBoxColumn
    Friend WithEvents Efectivo As DataGridViewComboBoxColumn
    Friend WithEvents Transferencia As DataGridViewComboBoxColumn
    Friend WithEvents Cheque As DataGridViewComboBoxColumn
    Friend WithEvents Terceros As DataGridViewComboBoxColumn
    Friend WithEvents TipPol As DataGridViewComboBoxColumn
    Friend WithEvents ID As DataGridViewTextBoxColumn
End Class

