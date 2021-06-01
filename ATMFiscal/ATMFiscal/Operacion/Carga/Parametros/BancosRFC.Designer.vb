<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BancosRFC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BancosRFC))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.LblFiltro = New System.Windows.Forms.Label()
        Me.TxtFiltro = New Telerik.WinControls.UI.RadTextBox()
        Me.RadRecibidas = New System.Windows.Forms.RadioButton()
        Me.RadEmitidas = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmdGuardar = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.CmdDuplicar = New Telerik.WinControls.UI.RadButton()
        Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
        Me.CmdEliminar = New Telerik.WinControls.UI.RadButton()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.Verif = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Clave = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bank = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Emit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fav = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.lstCliente = New ATMFiscal.Listas()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdDuplicar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEliminar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel1.Controls.Add(Me.LblFiltro)
        Me.RadPanel1.Controls.Add(Me.TxtFiltro)
        Me.RadPanel1.Controls.Add(Me.RadRecibidas)
        Me.RadPanel1.Controls.Add(Me.RadEmitidas)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label1)
        Me.RadPanel1.Controls.Add(Me.RadButton1)
        Me.RadPanel1.Controls.Add(Me.Label2)
        Me.RadPanel1.Controls.Add(Me.CmdGuardar)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Controls.Add(Me.CmdDuplicar)
        Me.RadPanel1.Controls.Add(Me.CmdImportar)
        Me.RadPanel1.Controls.Add(Me.CmdEliminar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1033, 101)
        Me.RadPanel1.TabIndex = 0
        '
        'LblFiltro
        '
        Me.LblFiltro.AutoSize = True
        Me.LblFiltro.BackColor = System.Drawing.Color.LightSteelBlue
        Me.LblFiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFiltro.Location = New System.Drawing.Point(898, 9)
        Me.LblFiltro.Name = "LblFiltro"
        Me.LblFiltro.Size = New System.Drawing.Size(0, 18)
        Me.LblFiltro.TabIndex = 518
        '
        'TxtFiltro
        '
        Me.TxtFiltro.AutoSize = False
        Me.TxtFiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFiltro.Location = New System.Drawing.Point(819, 33)
        Me.TxtFiltro.Name = "TxtFiltro"
        Me.TxtFiltro.Size = New System.Drawing.Size(196, 36)
        Me.TxtFiltro.TabIndex = 517
        Me.TxtFiltro.ThemeName = "Material"
        '
        'RadRecibidas
        '
        Me.RadRecibidas.AutoSize = True
        Me.RadRecibidas.Checked = True
        Me.RadRecibidas.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.RadRecibidas.ForeColor = System.Drawing.Color.DarkGreen
        Me.RadRecibidas.Location = New System.Drawing.Point(165, 71)
        Me.RadRecibidas.Name = "RadRecibidas"
        Me.RadRecibidas.Size = New System.Drawing.Size(97, 22)
        Me.RadRecibidas.TabIndex = 516
        Me.RadRecibidas.TabStop = True
        Me.RadRecibidas.Text = "Recibidas"
        Me.RadRecibidas.UseVisualStyleBackColor = True
        '
        'RadEmitidas
        '
        Me.RadEmitidas.AutoSize = True
        Me.RadEmitidas.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.RadEmitidas.ForeColor = System.Drawing.Color.Navy
        Me.RadEmitidas.Location = New System.Drawing.Point(25, 71)
        Me.RadEmitidas.Name = "RadEmitidas"
        Me.RadEmitidas.Size = New System.Drawing.Size(88, 22)
        Me.RadEmitidas.TabIndex = 515
        Me.RadEmitidas.Text = "Emitidas"
        Me.RadEmitidas.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(816, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 18)
        Me.Label1.TabIndex = 97
        Me.Label1.Text = "Filtrar:"
        '
        'RadButton1
        '
        Me.RadButton1.Image = Global.ATMFiscal.My.Resources.Resources.Manualito
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton1.Location = New System.Drawing.Point(319, 14)
        Me.RadButton1.Margin = New System.Windows.Forms.Padding(2)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(50, 54)
        Me.RadButton1.TabIndex = 512
        Me.RadButton1.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.RadButton1.ThemeName = "Aqua"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(373, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 18)
        Me.Label2.TabIndex = 97
        Me.Label2.Text = "Empresa:"
        '
        'CmdGuardar
        '
        Me.CmdGuardar.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardar.Location = New System.Drawing.Point(213, 14)
        Me.CmdGuardar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardar.Name = "CmdGuardar"
        Me.CmdGuardar.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardar.TabIndex = 514
        Me.CmdGuardar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardar.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = CType(resources.GetObject("cmdCerrar.Image"), System.Drawing.Image)
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(1, 14)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 509
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(54, 14)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 513
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'CmdDuplicar
        '
        Me.CmdDuplicar.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdDuplicar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdDuplicar.Location = New System.Drawing.Point(160, 14)
        Me.CmdDuplicar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdDuplicar.Name = "CmdDuplicar"
        Me.CmdDuplicar.Size = New System.Drawing.Size(50, 54)
        Me.CmdDuplicar.TabIndex = 512
        Me.CmdDuplicar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdDuplicar.ThemeName = "Aqua"
        '
        'CmdImportar
        '
        Me.CmdImportar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdImportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImportar.Location = New System.Drawing.Point(107, 14)
        Me.CmdImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImportar.Name = "CmdImportar"
        Me.CmdImportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdImportar.TabIndex = 510
        Me.CmdImportar.TabStop = False
        Me.CmdImportar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdImportar.ThemeName = "Aqua"
        '
        'CmdEliminar
        '
        Me.CmdEliminar.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminar.Location = New System.Drawing.Point(266, 14)
        Me.CmdEliminar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdEliminar.Name = "CmdEliminar"
        Me.CmdEliminar.Size = New System.Drawing.Size(50, 54)
        Me.CmdEliminar.TabIndex = 511
        Me.CmdEliminar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEliminar.ThemeName = "Aqua"
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TablaImportar.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Verif, Me.RF, Me.Nom, Me.Clave, Me.Bank, Me.Emit, Me.ID, Me.Fav})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TablaImportar.DefaultCellStyle = DataGridViewCellStyle2
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 101)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaImportar.Name = "TablaImportar"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TablaImportar.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(1033, 285)
        Me.TablaImportar.TabIndex = 2
        '
        'Verif
        '
        Me.Verif.HeaderText = "Lista"
        Me.Verif.Name = "Verif"
        Me.Verif.ReadOnly = True
        Me.Verif.Width = 55
        '
        'RF
        '
        Me.RF.HeaderText = "RFC"
        Me.RF.Name = "RF"
        Me.RF.ReadOnly = True
        Me.RF.Width = 72
        '
        'Nom
        '
        Me.Nom.HeaderText = "Nombre"
        Me.Nom.Name = "Nom"
        Me.Nom.ReadOnly = True
        Me.Nom.Width = 103
        '
        'Clave
        '
        Me.Clave.HeaderText = "Cuenta"
        Me.Clave.Name = "Clave"
        Me.Clave.Width = 96
        '
        'Bank
        '
        Me.Bank.HeaderText = "Banco"
        Me.Bank.Name = "Bank"
        Me.Bank.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Bank.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Bank.Width = 89
        '
        'Emit
        '
        Me.Emit.HeaderText = "Emitidas"
        Me.Emit.Name = "Emit"
        Me.Emit.ReadOnly = True
        Me.Emit.Visible = False
        Me.Emit.Width = 90
        '
        'ID
        '
        Me.ID.HeaderText = "Registro"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Visible = False
        Me.ID.Width = 90
        '
        'Fav
        '
        Me.Fav.HeaderText = "Favorito"
        Me.Fav.Name = "Fav"
        Me.Fav.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Fav.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Fav.Width = 106
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(377, 33)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(423, 36)
        Me.lstCliente.TabIndex = 98
        '
        'BancosRFC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1033, 386)
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BancosRFC"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bancos Emitidas / Recibidas"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdDuplicar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEliminar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdGuardar As Telerik.WinControls.UI.RadButton
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdDuplicar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdEliminar As Telerik.WinControls.UI.RadButton
	Friend WithEvents RadRecibidas As RadioButton
	Friend WithEvents RadEmitidas As RadioButton
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents TxtFiltro As Telerik.WinControls.UI.RadTextBox
	Friend WithEvents TablaImportar As DataGridView
	Friend WithEvents Verif As DataGridViewCheckBoxColumn
	Friend WithEvents RF As DataGridViewTextBoxColumn
	Friend WithEvents Nom As DataGridViewTextBoxColumn
	Friend WithEvents Clave As DataGridViewTextBoxColumn
	Friend WithEvents Bank As DataGridViewComboBoxColumn
	Friend WithEvents Emit As DataGridViewTextBoxColumn
	Friend WithEvents ID As DataGridViewTextBoxColumn
	Friend WithEvents Fav As DataGridViewCheckBoxColumn
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents LblFiltro As Label
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
End Class

