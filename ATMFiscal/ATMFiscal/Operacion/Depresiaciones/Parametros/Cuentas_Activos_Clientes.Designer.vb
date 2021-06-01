<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cuentas_Activos_Clientes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Cuentas_Activos_Clientes))
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmdEliminarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdGuardarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevoF = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarFact = New Telerik.WinControls.UI.RadButton()
        Me.CmdSalirF = New Telerik.WinControls.UI.RadButton()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaActivo = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Tipo = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtaDepAct = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Dep = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IDCta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResDep1 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IdResDep1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResDep2 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IdResDep2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResDep3 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IdResDep3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResDep4 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IdResDep4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.CmdEliminarF)
        Me.RadPanel1.Controls.Add(Me.CmdGuardarF)
        Me.RadPanel1.Controls.Add(Me.CmdNuevoF)
        Me.RadPanel1.Controls.Add(Me.CmdBuscarFact)
        Me.RadPanel1.Controls.Add(Me.CmdSalirF)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(808, 88)
        Me.RadPanel1.TabIndex = 3
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(306, 33)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(370, 36)
        Me.lstCliente.TabIndex = 649
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(302, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 648
        Me.Label3.Text = "Empresa:"
        '
        'CmdEliminarF
        '
        Me.CmdEliminarF.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminarF.Location = New System.Drawing.Point(164, 2)
        Me.CmdEliminarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdEliminarF.Name = "CmdEliminarF"
        Me.CmdEliminarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdEliminarF.TabIndex = 647
        Me.CmdEliminarF.TabStop = False
        Me.CmdEliminarF.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdEliminarF.ThemeName = "Aqua"
        '
        'CmdGuardarF
        '
        Me.CmdGuardarF.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardarF.Location = New System.Drawing.Point(218, 2)
        Me.CmdGuardarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardarF.Name = "CmdGuardarF"
        Me.CmdGuardarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardarF.TabIndex = 646
        Me.CmdGuardarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardarF.ThemeName = "Aqua"
        '
        'CmdNuevoF
        '
        Me.CmdNuevoF.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevoF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevoF.Location = New System.Drawing.Point(110, 2)
        Me.CmdNuevoF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdNuevoF.Name = "CmdNuevoF"
        Me.CmdNuevoF.Size = New System.Drawing.Size(50, 54)
        Me.CmdNuevoF.TabIndex = 645
        Me.CmdNuevoF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdNuevoF.ThemeName = "Aqua"
        '
        'CmdBuscarFact
        '
        Me.CmdBuscarFact.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscarFact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarFact.Location = New System.Drawing.Point(56, 2)
        Me.CmdBuscarFact.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarFact.Name = "CmdBuscarFact"
        Me.CmdBuscarFact.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscarFact.TabIndex = 644
        Me.CmdBuscarFact.TabStop = False
        Me.CmdBuscarFact.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdBuscarFact.ThemeName = "Aqua"
        '
        'CmdSalirF
        '
        Me.CmdSalirF.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.CmdSalirF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdSalirF.Location = New System.Drawing.Point(2, 2)
        Me.CmdSalirF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdSalirF.Name = "CmdSalirF"
        Me.CmdSalirF.Size = New System.Drawing.Size(50, 54)
        Me.CmdSalirF.TabIndex = 643
        Me.CmdSalirF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdSalirF.ThemeName = "Aqua"
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.CtaActivo, Me.Tipo, Me.CtaDepAct, Me.Dep, Me.IDCta, Me.ResDep1, Me.IdResDep1, Me.ResDep2, Me.IdResDep2, Me.ResDep3, Me.IdResDep3, Me.ResDep4, Me.IdResDep4})
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 88)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaImportar.Name = "TablaImportar"
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(808, 251)
        Me.TablaImportar.TabIndex = 4
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.Visible = False
        Me.ID.Width = 200
        '
        'CtaActivo
        '
        Me.CtaActivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.CtaActivo.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaActivo.HeaderText = "Cuenta"
        Me.CtaActivo.Name = "CtaActivo"
        Me.CtaActivo.Width = 73
        '
        'Tipo
        '
        Me.Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Tipo.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.Tipo.HeaderText = "Tipo"
        Me.Tipo.Name = "Tipo"
        Me.Tipo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tipo.Width = 50
        '
        'CtaDepAct
        '
        Me.CtaDepAct.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaDepAct.HeaderText = "Cuenta Depreciacion"
        Me.CtaDepAct.Name = "CtaDepAct"
        '
        'Dep
        '
        Me.Dep.HeaderText = "Depreciacion"
        Me.Dep.Name = "Dep"
        Me.Dep.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'IDCta
        '
        Me.IDCta.HeaderText = "IDCta"
        Me.IDCta.Name = "IDCta"
        Me.IDCta.Visible = False
        '
        'ResDep1
        '
        Me.ResDep1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ResDep1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.ResDep1.HeaderText = "Resultado_Dep_1"
        Me.ResDep1.Name = "ResDep1"
        Me.ResDep1.Width = 161
        '
        'IdResDep1
        '
        Me.IdResDep1.HeaderText = "IdResDep1"
        Me.IdResDep1.Name = "IdResDep1"
        Me.IdResDep1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IdResDep1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.IdResDep1.Visible = False
        '
        'ResDep2
        '
        Me.ResDep2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ResDep2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.ResDep2.HeaderText = "Resultado_Dep_2"
        Me.ResDep2.Name = "ResDep2"
        Me.ResDep2.Width = 161
        '
        'IdResDep2
        '
        Me.IdResDep2.HeaderText = "IdResDep2"
        Me.IdResDep2.Name = "IdResDep2"
        Me.IdResDep2.Visible = False
        '
        'ResDep3
        '
        Me.ResDep3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ResDep3.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.ResDep3.HeaderText = "Resultado_Dep_3"
        Me.ResDep3.Name = "ResDep3"
        Me.ResDep3.Width = 161
        '
        'IdResDep3
        '
        Me.IdResDep3.HeaderText = "IdResDep3"
        Me.IdResDep3.Name = "IdResDep3"
        '
        'ResDep4
        '
        Me.ResDep4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ResDep4.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.ResDep4.HeaderText = "Resultado_Dep_4"
        Me.ResDep4.Name = "ResDep4"
        Me.ResDep4.Width = 161
        '
        'IdResDep4
        '
        Me.IdResDep4.HeaderText = "IdResDep4"
        Me.IdResDep4.Name = "IdResDep4"
        Me.IdResDep4.Visible = False
        '
        'Cuentas_Activos_Clientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(808, 339)
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Cuentas_Activos_Clientes"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cuentas Activos Empresa"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Ayuda As ToolTip
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents CmdEliminarF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdGuardarF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdNuevoF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdBuscarFact As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdSalirF As Telerik.WinControls.UI.RadButton
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents TablaImportar As DataGridView
	Friend WithEvents ID As DataGridViewTextBoxColumn
	Friend WithEvents CtaActivo As DataGridViewComboBoxColumn
	Friend WithEvents Tipo As DataGridViewComboBoxColumn
	Friend WithEvents CtaDepAct As DataGridViewComboBoxColumn
	Friend WithEvents Dep As DataGridViewComboBoxColumn
	Friend WithEvents IDCta As DataGridViewTextBoxColumn
	Friend WithEvents ResDep1 As DataGridViewComboBoxColumn
	Friend WithEvents IdResDep1 As DataGridViewTextBoxColumn
	Friend WithEvents ResDep2 As DataGridViewComboBoxColumn
	Friend WithEvents IdResDep2 As DataGridViewTextBoxColumn
	Friend WithEvents ResDep3 As DataGridViewComboBoxColumn
	Friend WithEvents IdResDep3 As DataGridViewTextBoxColumn
	Friend WithEvents ResDep4 As DataGridViewComboBoxColumn
	Friend WithEvents IdResDep4 As DataGridViewTextBoxColumn


End Class

