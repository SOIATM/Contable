<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Liberar_UUID
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Liberar_UUID))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtFiltro = New Telerik.WinControls.UI.RadTextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DtInicio = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Dtfin = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ChkComplementos = New System.Windows.Forms.CheckBox()
        Me.RadEmitidas = New System.Windows.Forms.RadioButton()
        Me.ChkFacturas = New System.Windows.Forms.CheckBox()
        Me.RadRecibidas = New System.Windows.Forms.RadioButton()
        Me.LblFiltro = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
        Me.CmdEliminar = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.Verif = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.UUID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.anio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Num = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DtInicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dtfin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEliminar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TxtFiltro)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.LblFiltro)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.lstCliente)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.cmdCerrar)
        Me.Panel1.Controls.Add(Me.CmdImportar)
        Me.Panel1.Controls.Add(Me.CmdEliminar)
        Me.Panel1.Controls.Add(Me.CmdLimpiar)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1551, 187)
        Me.Panel1.TabIndex = 0
        '
        'TxtFiltro
        '
        Me.TxtFiltro.AutoSize = False
        Me.TxtFiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFiltro.Location = New System.Drawing.Point(262, 125)
        Me.TxtFiltro.Name = "TxtFiltro"
        Me.TxtFiltro.Size = New System.Drawing.Size(553, 36)
        Me.TxtFiltro.TabIndex = 644
        Me.TxtFiltro.ThemeName = "Material"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DtInicio)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Dtfin)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(891, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(631, 91)
        Me.GroupBox1.TabIndex = 643
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Periodo:     "
        '
        'DtInicio
        '
        Me.DtInicio.AutoSize = False
        Me.DtInicio.CalendarSize = New System.Drawing.Size(290, 320)
        Me.DtInicio.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtInicio.Location = New System.Drawing.Point(16, 41)
        Me.DtInicio.Name = "DtInicio"
        Me.DtInicio.Size = New System.Drawing.Size(299, 41)
        Me.DtInicio.TabIndex = 640
        Me.DtInicio.TabStop = False
        Me.DtInicio.Text = "viernes, 19 de febrero de 2021"
        Me.DtInicio.ThemeName = "MaterialBlueGrey"
        Me.DtInicio.Value = New Date(2021, 2, 19, 12, 2, 23, 431)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(114, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 18)
        Me.Label3.TabIndex = 638
        Me.Label3.Text = "Fecha Inicial:"
        '
        'Dtfin
        '
        Me.Dtfin.AutoSize = False
        Me.Dtfin.CalendarSize = New System.Drawing.Size(290, 320)
        Me.Dtfin.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtfin.Location = New System.Drawing.Point(327, 41)
        Me.Dtfin.Name = "Dtfin"
        Me.Dtfin.Size = New System.Drawing.Size(299, 41)
        Me.Dtfin.TabIndex = 641
        Me.Dtfin.TabStop = False
        Me.Dtfin.Text = "viernes, 19 de febrero de 2021"
        Me.Dtfin.ThemeName = "MaterialBlueGrey"
        Me.Dtfin.Value = New Date(2021, 2, 19, 12, 2, 23, 431)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(430, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 18)
        Me.Label4.TabIndex = 639
        Me.Label4.Text = "Fecha Final:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ChkComplementos)
        Me.GroupBox2.Controls.Add(Me.RadEmitidas)
        Me.GroupBox2.Controls.Add(Me.ChkFacturas)
        Me.GroupBox2.Controls.Add(Me.RadRecibidas)
        Me.GroupBox2.Location = New System.Drawing.Point(891, 108)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(631, 65)
        Me.GroupBox2.TabIndex = 642
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Parametros:     "
        '
        'ChkComplementos
        '
        Me.ChkComplementos.AutoSize = True
        Me.ChkComplementos.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkComplementos.Location = New System.Drawing.Point(162, 29)
        Me.ChkComplementos.Name = "ChkComplementos"
        Me.ChkComplementos.Size = New System.Drawing.Size(143, 24)
        Me.ChkComplementos.TabIndex = 517
        Me.ChkComplementos.Text = "Complementos"
        Me.ChkComplementos.UseVisualStyleBackColor = True
        '
        'RadEmitidas
        '
        Me.RadEmitidas.AutoSize = True
        Me.RadEmitidas.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadEmitidas.ForeColor = System.Drawing.Color.Navy
        Me.RadEmitidas.Location = New System.Drawing.Point(356, 29)
        Me.RadEmitidas.Name = "RadEmitidas"
        Me.RadEmitidas.Size = New System.Drawing.Size(103, 24)
        Me.RadEmitidas.TabIndex = 52
        Me.RadEmitidas.Text = "Emitidas"
        Me.RadEmitidas.UseVisualStyleBackColor = True
        '
        'ChkFacturas
        '
        Me.ChkFacturas.AutoSize = True
        Me.ChkFacturas.Checked = True
        Me.ChkFacturas.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkFacturas.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkFacturas.Location = New System.Drawing.Point(14, 29)
        Me.ChkFacturas.Name = "ChkFacturas"
        Me.ChkFacturas.Size = New System.Drawing.Size(97, 24)
        Me.ChkFacturas.TabIndex = 516
        Me.ChkFacturas.Text = "Facturas"
        Me.ChkFacturas.UseVisualStyleBackColor = True
        '
        'RadRecibidas
        '
        Me.RadRecibidas.AutoSize = True
        Me.RadRecibidas.Checked = True
        Me.RadRecibidas.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadRecibidas.ForeColor = System.Drawing.Color.DarkGreen
        Me.RadRecibidas.Location = New System.Drawing.Point(510, 29)
        Me.RadRecibidas.Name = "RadRecibidas"
        Me.RadRecibidas.Size = New System.Drawing.Size(113, 24)
        Me.RadRecibidas.TabIndex = 53
        Me.RadRecibidas.TabStop = True
        Me.RadRecibidas.Text = "Recibidas"
        Me.RadRecibidas.UseVisualStyleBackColor = True
        '
        'LblFiltro
        '
        Me.LblFiltro.AutoSize = True
        Me.LblFiltro.BackColor = System.Drawing.Color.LightSteelBlue
        Me.LblFiltro.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFiltro.Location = New System.Drawing.Point(344, 102)
        Me.LblFiltro.Name = "LblFiltro"
        Me.LblFiltro.Size = New System.Drawing.Size(54, 20)
        Me.LblFiltro.TabIndex = 603
        Me.LblFiltro.Text = "UUID"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(258, 102)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 20)
        Me.Label6.TabIndex = 602
        Me.Label6.Text = "Filtrar:"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(262, 43)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(553, 36)
        Me.lstCliente.TabIndex = 601
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label20.Location = New System.Drawing.Point(259, 17)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(72, 18)
        Me.Label20.TabIndex = 600
        Me.Label20.Text = "Empresa:"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(11, 11)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 599
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdImportar
        '
        Me.CmdImportar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdImportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImportar.Location = New System.Drawing.Point(119, 11)
        Me.CmdImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImportar.Name = "CmdImportar"
        Me.CmdImportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdImportar.TabIndex = 596
        Me.CmdImportar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdImportar.ThemeName = "Aqua"
        '
        'CmdEliminar
        '
        Me.CmdEliminar.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminar.Location = New System.Drawing.Point(173, 11)
        Me.CmdEliminar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdEliminar.Name = "CmdEliminar"
        Me.CmdEliminar.Size = New System.Drawing.Size(50, 54)
        Me.CmdEliminar.TabIndex = 598
        Me.CmdEliminar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEliminar.ThemeName = "Aqua"
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(65, 11)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 597
        Me.CmdLimpiar.TabStop = False
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TablaImportar)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 187)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1551, 503)
        Me.Panel2.TabIndex = 1
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Verif, Me.UUID, Me.anio, Me.Mes, Me.Num, Me.Pol})
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 0)
        Me.TablaImportar.Name = "TablaImportar"
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(1551, 503)
        Me.TablaImportar.TabIndex = 3
        '
        'Verif
        '
        Me.Verif.HeaderText = "Seleccion"
        Me.Verif.Name = "Verif"
        '
        'UUID
        '
        Me.UUID.HeaderText = "UUID"
        Me.UUID.Name = "UUID"
        Me.UUID.ReadOnly = True
        '
        'anio
        '
        Me.anio.HeaderText = "Año"
        Me.anio.Name = "anio"
        '
        'Mes
        '
        Me.Mes.HeaderText = "Mes"
        Me.Mes.Name = "Mes"
        '
        'Num
        '
        Me.Num.HeaderText = "Numero"
        Me.Num.Name = "Num"
        '
        'Pol
        '
        Me.Pol.HeaderText = "Poliza"
        Me.Pol.Name = "Pol"
        Me.Pol.ReadOnly = True
        '
        'Liberar_UUID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1551, 690)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Liberar_UUID"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "P_Importar"
        Me.Text = "Liberar XML por UUID"
        Me.ThemeName = "MaterialBlueGrey"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DtInicio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dtfin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEliminar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents Panel1 As Panel
    Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdEliminar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
    Friend WithEvents LblFiltro As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label20 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DtInicio As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Dtfin As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ChkComplementos As CheckBox
    Friend WithEvents RadEmitidas As RadioButton
    Friend WithEvents ChkFacturas As CheckBox
    Friend WithEvents RadRecibidas As RadioButton
    Friend WithEvents TxtFiltro As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TablaImportar As DataGridView
    Friend WithEvents Verif As DataGridViewCheckBoxColumn
    Friend WithEvents UUID As DataGridViewTextBoxColumn
    Friend WithEvents anio As DataGridViewTextBoxColumn
    Friend WithEvents Mes As DataGridViewTextBoxColumn
    Friend WithEvents Num As DataGridViewTextBoxColumn
    Friend WithEvents Pol As DataGridViewTextBoxColumn
End Class

