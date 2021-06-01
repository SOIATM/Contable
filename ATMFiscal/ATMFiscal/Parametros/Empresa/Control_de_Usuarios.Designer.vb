<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Control_de_Usuarios
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Control_de_Usuarios))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.TablaUsuarios = New ATMFiscal.Tabla_Filtro()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lstArea = New ATMFiscal.Listas()
        Me.lstExtencion = New ATMFiscal.Listas()
        Me.LstTelefono = New ATMFiscal.Listas()
        Me.LstCorreo = New ATMFiscal.Listas()
        Me.lstEmpresa = New ATMFiscal.Listas()
        Me.lstUsuarios = New ATMFiscal.Listas()
        Me.lstMatricula = New ATMFiscal.Listas()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.LstImportar = New ATMFiscal.Listas()
        Me.LstParametros = New ATMFiscal.Listas()
        Me.LstAnual = New ATMFiscal.Listas()
        Me.LstImps = New ATMFiscal.Listas()
        Me.LstDep = New ATMFiscal.Listas()
        Me.LstAuditoria = New ATMFiscal.Listas()
        Me.LstReportes = New ATMFiscal.Listas()
        Me.lstTotal = New ATMFiscal.Listas()
        Me.lstMaster = New ATMFiscal.Listas()
        Me.LstAcceso = New ATMFiscal.Listas()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.TablaUsuarios)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1448, 291)
        Me.RadPanel1.TabIndex = 0
        '
        'TablaUsuarios
        '
        Me.TablaUsuarios.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TablaUsuarios.CmdActualizar_enabled = True
        Me.TablaUsuarios.Cmdcerrar_enabled = True
        Me.TablaUsuarios.CmdEliminar_enabled = True
        Me.TablaUsuarios.CmdExportaExcel_enabled = True
        Me.TablaUsuarios.Cmdguardar_enabled = True
        Me.TablaUsuarios.CmdNuevo_enabled = True
        Me.TablaUsuarios.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaUsuarios.Location = New System.Drawing.Point(0, 0)
        Me.TablaUsuarios.Name = "TablaUsuarios"
        Me.TablaUsuarios.Size = New System.Drawing.Size(1448, 291)
        Me.TablaUsuarios.SqlSelect = "select"
        Me.TablaUsuarios.TabIndex = 0
        Me.TablaUsuarios.Tag = "P_Unplus"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.lstArea)
        Me.RadGroupBox1.Controls.Add(Me.lstExtencion)
        Me.RadGroupBox1.Controls.Add(Me.LstTelefono)
        Me.RadGroupBox1.Controls.Add(Me.LstCorreo)
        Me.RadGroupBox1.Controls.Add(Me.lstEmpresa)
        Me.RadGroupBox1.Controls.Add(Me.lstUsuarios)
        Me.RadGroupBox1.Controls.Add(Me.lstMatricula)
        Me.RadGroupBox1.Controls.Add(Me.Label8)
        Me.RadGroupBox1.Controls.Add(Me.Label7)
        Me.RadGroupBox1.Controls.Add(Me.Label6)
        Me.RadGroupBox1.Controls.Add(Me.Label5)
        Me.RadGroupBox1.Controls.Add(Me.Label3)
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Control Datos Personales"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 308)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(1090, 171)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = "Control Datos Personales"
        Me.RadGroupBox1.ThemeName = "MaterialBlueGrey"
        '
        'lstArea
        '
        Me.lstArea.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstArea.Location = New System.Drawing.Point(19, 117)
        Me.lstArea.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstArea.Name = "lstArea"
        Me.lstArea.SelectItem = ""
        Me.lstArea.SelectText = ""
        Me.lstArea.Size = New System.Drawing.Size(204, 36)
        Me.lstArea.TabIndex = 99
        '
        'lstExtencion
        '
        Me.lstExtencion.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstExtencion.Location = New System.Drawing.Point(480, 117)
        Me.lstExtencion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstExtencion.Name = "lstExtencion"
        Me.lstExtencion.SelectItem = ""
        Me.lstExtencion.SelectText = ""
        Me.lstExtencion.Size = New System.Drawing.Size(111, 36)
        Me.lstExtencion.TabIndex = 98
        '
        'LstTelefono
        '
        Me.LstTelefono.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstTelefono.Location = New System.Drawing.Point(244, 117)
        Me.LstTelefono.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstTelefono.Name = "LstTelefono"
        Me.LstTelefono.SelectItem = ""
        Me.LstTelefono.SelectText = ""
        Me.LstTelefono.Size = New System.Drawing.Size(212, 36)
        Me.LstTelefono.TabIndex = 97
        '
        'LstCorreo
        '
        Me.LstCorreo.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstCorreo.Location = New System.Drawing.Point(622, 117)
        Me.LstCorreo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstCorreo.Name = "LstCorreo"
        Me.LstCorreo.SelectItem = ""
        Me.LstCorreo.SelectText = ""
        Me.LstCorreo.Size = New System.Drawing.Size(446, 36)
        Me.LstCorreo.TabIndex = 96
        '
        'lstEmpresa
        '
        Me.lstEmpresa.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstEmpresa.Location = New System.Drawing.Point(622, 53)
        Me.lstEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstEmpresa.Name = "lstEmpresa"
        Me.lstEmpresa.SelectItem = ""
        Me.lstEmpresa.SelectText = ""
        Me.lstEmpresa.Size = New System.Drawing.Size(446, 36)
        Me.lstEmpresa.TabIndex = 94
        '
        'lstUsuarios
        '
        Me.lstUsuarios.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstUsuarios.Location = New System.Drawing.Point(400, 53)
        Me.lstUsuarios.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstUsuarios.Name = "lstUsuarios"
        Me.lstUsuarios.SelectItem = ""
        Me.lstUsuarios.SelectText = ""
        Me.lstUsuarios.Size = New System.Drawing.Size(205, 36)
        Me.lstUsuarios.TabIndex = 93
        '
        'lstMatricula
        '
        Me.lstMatricula.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstMatricula.Location = New System.Drawing.Point(19, 53)
        Me.lstMatricula.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstMatricula.Name = "lstMatricula"
        Me.lstMatricula.SelectItem = ""
        Me.lstMatricula.SelectText = ""
        Me.lstMatricula.Size = New System.Drawing.Size(367, 36)
        Me.lstMatricula.TabIndex = 92
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(618, 93)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 18)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Correo:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(476, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 18)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Ext:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(240, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 18)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Teléfono:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 18)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Area:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(618, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Empresa:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 18)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Personal:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(396, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 18)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Usuario:"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox2.Controls.Add(Me.LstImportar)
        Me.RadGroupBox2.Controls.Add(Me.LstParametros)
        Me.RadGroupBox2.Controls.Add(Me.LstAnual)
        Me.RadGroupBox2.Controls.Add(Me.LstImps)
        Me.RadGroupBox2.Controls.Add(Me.LstDep)
        Me.RadGroupBox2.Controls.Add(Me.LstAuditoria)
        Me.RadGroupBox2.Controls.Add(Me.LstReportes)
        Me.RadGroupBox2.Controls.Add(Me.lstTotal)
        Me.RadGroupBox2.Controls.Add(Me.lstMaster)
        Me.RadGroupBox2.Controls.Add(Me.LstAcceso)
        Me.RadGroupBox2.Controls.Add(Me.Label17)
        Me.RadGroupBox2.Controls.Add(Me.Label18)
        Me.RadGroupBox2.Controls.Add(Me.Label16)
        Me.RadGroupBox2.Controls.Add(Me.Label15)
        Me.RadGroupBox2.Controls.Add(Me.Label14)
        Me.RadGroupBox2.Controls.Add(Me.Label13)
        Me.RadGroupBox2.Controls.Add(Me.Label10)
        Me.RadGroupBox2.Controls.Add(Me.Label12)
        Me.RadGroupBox2.Controls.Add(Me.Label9)
        Me.RadGroupBox2.Controls.Add(Me.Label11)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Control Acceso"
        Me.RadGroupBox2.Location = New System.Drawing.Point(12, 485)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(838, 193)
        Me.RadGroupBox2.TabIndex = 2
        Me.RadGroupBox2.Text = "Control Acceso"
        Me.RadGroupBox2.ThemeName = "MaterialBlueGrey"
        '
        'LstImportar
        '
        Me.LstImportar.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstImportar.Location = New System.Drawing.Point(655, 131)
        Me.LstImportar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstImportar.Name = "LstImportar"
        Me.LstImportar.SelectItem = ""
        Me.LstImportar.SelectText = ""
        Me.LstImportar.Size = New System.Drawing.Size(150, 36)
        Me.LstImportar.TabIndex = 109
        '
        'LstParametros
        '
        Me.LstParametros.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstParametros.Location = New System.Drawing.Point(496, 131)
        Me.LstParametros.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstParametros.Name = "LstParametros"
        Me.LstParametros.SelectItem = ""
        Me.LstParametros.SelectText = ""
        Me.LstParametros.Size = New System.Drawing.Size(150, 36)
        Me.LstParametros.TabIndex = 108
        '
        'LstAnual
        '
        Me.LstAnual.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstAnual.Location = New System.Drawing.Point(337, 131)
        Me.LstAnual.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstAnual.Name = "LstAnual"
        Me.LstAnual.SelectItem = ""
        Me.LstAnual.SelectText = ""
        Me.LstAnual.Size = New System.Drawing.Size(150, 36)
        Me.LstAnual.TabIndex = 107
        '
        'LstImps
        '
        Me.LstImps.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstImps.Location = New System.Drawing.Point(178, 131)
        Me.LstImps.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstImps.Name = "LstImps"
        Me.LstImps.SelectItem = ""
        Me.LstImps.SelectText = ""
        Me.LstImps.Size = New System.Drawing.Size(150, 36)
        Me.LstImps.TabIndex = 106
        '
        'LstDep
        '
        Me.LstDep.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstDep.Location = New System.Drawing.Point(19, 131)
        Me.LstDep.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstDep.Name = "LstDep"
        Me.LstDep.SelectItem = ""
        Me.LstDep.SelectText = ""
        Me.LstDep.Size = New System.Drawing.Size(150, 36)
        Me.LstDep.TabIndex = 105
        '
        'LstAuditoria
        '
        Me.LstAuditoria.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstAuditoria.Location = New System.Drawing.Point(658, 59)
        Me.LstAuditoria.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstAuditoria.Name = "LstAuditoria"
        Me.LstAuditoria.SelectItem = ""
        Me.LstAuditoria.SelectText = ""
        Me.LstAuditoria.Size = New System.Drawing.Size(150, 36)
        Me.LstAuditoria.TabIndex = 104
        '
        'LstReportes
        '
        Me.LstReportes.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstReportes.Location = New System.Drawing.Point(498, 59)
        Me.LstReportes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstReportes.Name = "LstReportes"
        Me.LstReportes.SelectItem = ""
        Me.LstReportes.SelectText = ""
        Me.LstReportes.Size = New System.Drawing.Size(150, 36)
        Me.LstReportes.TabIndex = 103
        '
        'lstTotal
        '
        Me.lstTotal.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstTotal.Location = New System.Drawing.Point(338, 59)
        Me.lstTotal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstTotal.Name = "lstTotal"
        Me.lstTotal.SelectItem = ""
        Me.lstTotal.SelectText = ""
        Me.lstTotal.Size = New System.Drawing.Size(150, 36)
        Me.lstTotal.TabIndex = 102
        '
        'lstMaster
        '
        Me.lstMaster.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstMaster.Location = New System.Drawing.Point(178, 60)
        Me.lstMaster.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstMaster.Name = "lstMaster"
        Me.lstMaster.SelectItem = ""
        Me.lstMaster.SelectText = ""
        Me.lstMaster.Size = New System.Drawing.Size(150, 36)
        Me.lstMaster.TabIndex = 101
        '
        'LstAcceso
        '
        Me.LstAcceso.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstAcceso.Location = New System.Drawing.Point(18, 60)
        Me.LstAcceso.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstAcceso.Name = "LstAcceso"
        Me.LstAcceso.SelectItem = ""
        Me.LstAcceso.SelectText = ""
        Me.LstAcceso.Size = New System.Drawing.Size(150, 36)
        Me.LstAcceso.TabIndex = 100
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Maroon
        Me.Label17.Location = New System.Drawing.Point(652, 109)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(92, 18)
        Me.Label17.TabIndex = 41
        Me.Label17.Text = "Importar Inf"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Maroon
        Me.Label18.Location = New System.Drawing.Point(655, 35)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(124, 18)
        Me.Label18.TabIndex = 40
        Me.Label18.Text = "Acceso Auditoría"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Maroon
        Me.Label16.Location = New System.Drawing.Point(493, 109)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(87, 18)
        Me.Label16.TabIndex = 39
        Me.Label16.Text = "Parámetros"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Maroon
        Me.Label15.Location = New System.Drawing.Point(334, 109)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(48, 18)
        Me.Label15.TabIndex = 38
        Me.Label15.Text = "Anual"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Maroon
        Me.Label14.Location = New System.Drawing.Point(175, 109)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(78, 18)
        Me.Label14.TabIndex = 37
        Me.Label14.Text = "Impuestos"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Maroon
        Me.Label13.Location = New System.Drawing.Point(16, 109)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(114, 18)
        Me.Label13.TabIndex = 36
        Me.Label13.Text = "Depreciaciones"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Maroon
        Me.Label10.Location = New System.Drawing.Point(495, 35)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(122, 18)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "Acceso Reportes"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Maroon
        Me.Label12.Location = New System.Drawing.Point(335, 35)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(94, 18)
        Me.Label12.TabIndex = 34
        Me.Label12.Text = "Acceso Total"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Maroon
        Me.Label9.Location = New System.Drawing.Point(175, 35)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 18)
        Me.Label9.TabIndex = 33
        Me.Label9.Text = "Master"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Maroon
        Me.Label11.Location = New System.Drawing.Point(16, 35)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(132, 18)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "Acceso al Sistema"
        '
        'Control_de_Usuarios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(1448, 724)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Control_de_Usuarios"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Control de Usuarios"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents TablaUsuarios As Tabla_Filtro
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lstMatricula As Listas
    Friend WithEvents lstEmpresa As Listas
    Friend WithEvents lstUsuarios As Listas
    Friend WithEvents lstArea As Listas
    Friend WithEvents lstExtencion As Listas
    Friend WithEvents LstTelefono As Listas
    Friend WithEvents LstCorreo As Listas
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents LstImportar As Listas
    Friend WithEvents LstParametros As Listas
    Friend WithEvents LstAnual As Listas
    Friend WithEvents LstImps As Listas
    Friend WithEvents LstDep As Listas
    Friend WithEvents LstAuditoria As Listas
    Friend WithEvents LstReportes As Listas
    Friend WithEvents lstTotal As Listas
    Friend WithEvents lstMaster As Listas
    Friend WithEvents LstAcceso As Listas
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label11 As Label
End Class

