<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Catalogo_Cuentas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Catalogo_Cuentas))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.CmdQuitar = New Telerik.WinControls.UI.RadButton()
        Me.LblFiltro = New System.Windows.Forms.Label()
        Me.TxtFiltro = New Telerik.WinControls.UI.RadTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LstClientes = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdActualizar = New Telerik.WinControls.UI.RadButton()
        Me.CmdSaldos = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdExp = New Telerik.WinControls.UI.RadButton()
        Me.TablaCat = New System.Windows.Forms.DataGridView()
        Me.MenuCatalogoCuentas = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditarCuentaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NuevaCuentaMadreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarCuentaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdQuitar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdActualizar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdSaldos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaCat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuCatalogoCuentas.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.CmdQuitar)
        Me.RadPanel1.Controls.Add(Me.LblFiltro)
        Me.RadPanel1.Controls.Add(Me.TxtFiltro)
        Me.RadPanel1.Controls.Add(Me.Label1)
        Me.RadPanel1.Controls.Add(Me.LstClientes)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.cmdActualizar)
        Me.RadPanel1.Controls.Add(Me.CmdSaldos)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdExp)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(702, 143)
        Me.RadPanel1.TabIndex = 0
        '
        'CmdQuitar
        '
        Me.CmdQuitar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdQuitar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.CmdQuitar.Location = New System.Drawing.Point(2, 71)
        Me.CmdQuitar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdQuitar.Name = "CmdQuitar"
        Me.CmdQuitar.Size = New System.Drawing.Size(212, 54)
        Me.CmdQuitar.TabIndex = 589
        Me.CmdQuitar.TabStop = False
        Me.CmdQuitar.Text = "Limpiar Cuentas Duplicadas"
        Me.CmdQuitar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdQuitar.ThemeName = "Aqua"
        '
        'LblFiltro
        '
        Me.LblFiltro.AutoSize = True
        Me.LblFiltro.BackColor = System.Drawing.Color.LightSteelBlue
        Me.LblFiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFiltro.Location = New System.Drawing.Point(295, 69)
        Me.LblFiltro.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblFiltro.Name = "LblFiltro"
        Me.LblFiltro.Size = New System.Drawing.Size(0, 18)
        Me.LblFiltro.TabIndex = 588
        '
        'TxtFiltro
        '
        Me.TxtFiltro.AutoSize = False
        Me.TxtFiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFiltro.Location = New System.Drawing.Point(239, 100)
        Me.TxtFiltro.Name = "TxtFiltro"
        Me.TxtFiltro.Size = New System.Drawing.Size(370, 36)
        Me.TxtFiltro.TabIndex = 587
        Me.TxtFiltro.ThemeName = "Material"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(236, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 18)
        Me.Label1.TabIndex = 586
        Me.Label1.Text = "Filtrar:"
        '
        'LstClientes
        '
        Me.LstClientes.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstClientes.Location = New System.Drawing.Point(239, 28)
        Me.LstClientes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstClientes.Name = "LstClientes"
        Me.LstClientes.SelectItem = ""
        Me.LstClientes.SelectText = ""
        Me.LstClientes.Size = New System.Drawing.Size(370, 36)
        Me.LstClientes.TabIndex = 585
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(236, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 584
        Me.Label3.Text = "Empresa:"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.cmdActualizar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdActualizar.Location = New System.Drawing.Point(56, 2)
        Me.cmdActualizar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdActualizar.Name = "cmdActualizar"
        Me.cmdActualizar.Size = New System.Drawing.Size(50, 54)
        Me.cmdActualizar.TabIndex = 582
        Me.cmdActualizar.TabStop = False
        Me.cmdActualizar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.cmdActualizar.ThemeName = "Aqua"
        '
        'CmdSaldos
        '
        Me.CmdSaldos.Image = Global.ATMFiscal.My.Resources.Resources.CatalogoCuentas
        Me.CmdSaldos.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdSaldos.Location = New System.Drawing.Point(164, 2)
        Me.CmdSaldos.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdSaldos.Name = "CmdSaldos"
        Me.CmdSaldos.Size = New System.Drawing.Size(50, 54)
        Me.CmdSaldos.TabIndex = 582
        Me.CmdSaldos.TabStop = False
        Me.CmdSaldos.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdSaldos.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(2, 2)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 583
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdExp
        '
        Me.CmdExp.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.CmdExp.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdExp.Location = New System.Drawing.Point(110, 2)
        Me.CmdExp.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdExp.Name = "CmdExp"
        Me.CmdExp.Size = New System.Drawing.Size(50, 54)
        Me.CmdExp.TabIndex = 583
        Me.CmdExp.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdExp.ThemeName = "Aqua"
        '
        'TablaCat
        '
        Me.TablaCat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.TablaCat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaCat.ContextMenuStrip = Me.MenuCatalogoCuentas
        Me.TablaCat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaCat.Location = New System.Drawing.Point(0, 143)
        Me.TablaCat.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaCat.Name = "TablaCat"
        Me.TablaCat.ReadOnly = True
        Me.TablaCat.RowTemplate.Height = 24
        Me.TablaCat.Size = New System.Drawing.Size(702, 298)
        Me.TablaCat.TabIndex = 2
        '
        'MenuCatalogoCuentas
        '
        Me.MenuCatalogoCuentas.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuCatalogoCuentas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.EditarCuentaToolStripMenuItem, Me.NuevaCuentaMadreToolStripMenuItem, Me.EliminarCuentaToolStripMenuItem})
        Me.MenuCatalogoCuentas.Name = "MenuCatalogoCuentasRFC"
        Me.MenuCatalogoCuentas.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuCatalogoCuentas.Size = New System.Drawing.Size(273, 100)
        Me.MenuCatalogoCuentas.Text = "Opciones de Registro"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.Insert
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(272, 24)
        Me.ToolStripMenuItem1.Text = "Nueva Cuenta"
        '
        'EditarCuentaToolStripMenuItem
        '
        Me.EditarCuentaToolStripMenuItem.Name = "EditarCuentaToolStripMenuItem"
        Me.EditarCuentaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.EditarCuentaToolStripMenuItem.Size = New System.Drawing.Size(272, 24)
        Me.EditarCuentaToolStripMenuItem.Text = "Editar Cuenta"
        '
        'NuevaCuentaMadreToolStripMenuItem
        '
        Me.NuevaCuentaMadreToolStripMenuItem.Name = "NuevaCuentaMadreToolStripMenuItem"
        Me.NuevaCuentaMadreToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.NuevaCuentaMadreToolStripMenuItem.Size = New System.Drawing.Size(272, 24)
        Me.NuevaCuentaMadreToolStripMenuItem.Text = "Nueva Cuenta Madre"
        '
        'EliminarCuentaToolStripMenuItem
        '
        Me.EliminarCuentaToolStripMenuItem.Name = "EliminarCuentaToolStripMenuItem"
        Me.EliminarCuentaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.EliminarCuentaToolStripMenuItem.Size = New System.Drawing.Size(272, 24)
        Me.EliminarCuentaToolStripMenuItem.Text = "Eliminar Cuenta"
        '
        'Catalogo_Cuentas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(702, 441)
        Me.Controls.Add(Me.TablaCat)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Catalogo_Cuentas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Catálogo de Cuentas"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdQuitar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdActualizar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdSaldos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaCat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuCatalogoCuentas.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents cmdActualizar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdSaldos As Telerik.WinControls.UI.RadButton
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdExp As Telerik.WinControls.UI.RadButton
	Friend WithEvents Label1 As Label
	Friend WithEvents LstClientes As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents TablaCat As DataGridView
	Friend WithEvents TxtFiltro As Telerik.WinControls.UI.RadTextBox
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents LblFiltro As Label
	Friend WithEvents MenuCatalogoCuentas As ContextMenuStrip
	Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
	Friend WithEvents EditarCuentaToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents NuevaCuentaMadreToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents EliminarCuentaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CmdQuitar As Telerik.WinControls.UI.RadButton
End Class

