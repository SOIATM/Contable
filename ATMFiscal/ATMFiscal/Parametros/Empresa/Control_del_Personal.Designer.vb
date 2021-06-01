<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Control_del_Personal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Control_del_Personal))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.DTFechaNacimiento = New Telerik.WinControls.UI.RadMaskedEditBox()
        Me.TxtIfe = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtRfc = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtCurp = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtTelefono = New Telerik.WinControls.UI.RadTextBox()
        Me.txtDireccion = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtApMaterno = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtApPaterno = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtNombre = New Telerik.WinControls.UI.RadTextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.TxtImss = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtSueldo = New Telerik.WinControls.UI.RadTextBox()
        Me.DtFechaBajaImss = New Telerik.WinControls.UI.RadMaskedEditBox()
        Me.dtFechaBaja = New Telerik.WinControls.UI.RadMaskedEditBox()
        Me.DtAltaIMSS = New Telerik.WinControls.UI.RadMaskedEditBox()
        Me.DtIngreso = New Telerik.WinControls.UI.RadMaskedEditBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.LstPuesto = New ATMFiscal.Listas()
        Me.lstNombre = New ATMFiscal.Listas()
        Me.LstEmpresa = New ATMFiscal.Listas()
        Me.lstMatricula = New ATMFiscal.Listas()
        Me.LstCp = New ATMFiscal.Listas()
        Me.lstEdoCivil = New ATMFiscal.Listas()
        Me.lstSexo = New ATMFiscal.Listas()
        Me.TablaPersonal = New ATMFiscal.Tabla_Filtro()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.DTFechaNacimiento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtIfe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRfc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCurp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTelefono, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDireccion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtApMaterno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtApPaterno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNombre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.TxtImss, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSueldo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtFechaBajaImss, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaBaja, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtAltaIMSS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtIngreso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.AutoScroll = True
        Me.RadPanel1.Controls.Add(Me.TablaPersonal)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1731, 362)
        Me.RadPanel1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.DTFechaNacimiento)
        Me.RadGroupBox1.Controls.Add(Me.LstCp)
        Me.RadGroupBox1.Controls.Add(Me.lstEdoCivil)
        Me.RadGroupBox1.Controls.Add(Me.lstSexo)
        Me.RadGroupBox1.Controls.Add(Me.TxtIfe)
        Me.RadGroupBox1.Controls.Add(Me.TxtRfc)
        Me.RadGroupBox1.Controls.Add(Me.TxtCurp)
        Me.RadGroupBox1.Controls.Add(Me.TxtTelefono)
        Me.RadGroupBox1.Controls.Add(Me.txtDireccion)
        Me.RadGroupBox1.Controls.Add(Me.TxtApMaterno)
        Me.RadGroupBox1.Controls.Add(Me.TxtApPaterno)
        Me.RadGroupBox1.Controls.Add(Me.TxtNombre)
        Me.RadGroupBox1.Controls.Add(Me.Label14)
        Me.RadGroupBox1.Controls.Add(Me.Label13)
        Me.RadGroupBox1.Controls.Add(Me.Label12)
        Me.RadGroupBox1.Controls.Add(Me.Label11)
        Me.RadGroupBox1.Controls.Add(Me.Label10)
        Me.RadGroupBox1.Controls.Add(Me.Label9)
        Me.RadGroupBox1.Controls.Add(Me.Label8)
        Me.RadGroupBox1.Controls.Add(Me.Label7)
        Me.RadGroupBox1.Controls.Add(Me.Label6)
        Me.RadGroupBox1.Controls.Add(Me.Label5)
        Me.RadGroupBox1.Controls.Add(Me.Label4)
        Me.RadGroupBox1.Controls.Add(Me.Label3)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!)
        Me.RadGroupBox1.HeaderText = "Datos Personales"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 380)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(856, 369)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = "Datos Personales"
        Me.RadGroupBox1.ThemeName = "MaterialBlueGrey"
        '
        'DTFechaNacimiento
        '
        Me.DTFechaNacimiento.AutoSize = False
        Me.DTFechaNacimiento.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.DTFechaNacimiento.Location = New System.Drawing.Point(17, 134)
        Me.DTFechaNacimiento.Mask = "00/00/0000"
        Me.DTFechaNacimiento.Name = "DTFechaNacimiento"
        Me.DTFechaNacimiento.Size = New System.Drawing.Size(207, 36)
        Me.DTFechaNacimiento.TabIndex = 76
        Me.DTFechaNacimiento.TabStop = False
        '
        'TxtIfe
        '
        Me.TxtIfe.AutoSize = False
        Me.TxtIfe.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.TxtIfe.Location = New System.Drawing.Point(17, 317)
        Me.TxtIfe.Name = "TxtIfe"
        Me.TxtIfe.Size = New System.Drawing.Size(367, 36)
        Me.TxtIfe.TabIndex = 73
        Me.TxtIfe.ThemeName = "Material"
        '
        'TxtRfc
        '
        Me.TxtRfc.AutoSize = False
        Me.TxtRfc.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.TxtRfc.Location = New System.Drawing.Point(581, 254)
        Me.TxtRfc.Name = "TxtRfc"
        Me.TxtRfc.Size = New System.Drawing.Size(260, 36)
        Me.TxtRfc.TabIndex = 72
        Me.TxtRfc.ThemeName = "Material"
        '
        'TxtCurp
        '
        Me.TxtCurp.AutoSize = False
        Me.TxtCurp.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.TxtCurp.Location = New System.Drawing.Point(299, 252)
        Me.TxtCurp.Name = "TxtCurp"
        Me.TxtCurp.Size = New System.Drawing.Size(260, 36)
        Me.TxtCurp.TabIndex = 71
        Me.TxtCurp.ThemeName = "Material"
        '
        'TxtTelefono
        '
        Me.TxtTelefono.AutoSize = False
        Me.TxtTelefono.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.TxtTelefono.Location = New System.Drawing.Point(17, 254)
        Me.TxtTelefono.Name = "TxtTelefono"
        Me.TxtTelefono.Size = New System.Drawing.Size(260, 36)
        Me.TxtTelefono.TabIndex = 4
        Me.TxtTelefono.ThemeName = "Material"
        '
        'txtDireccion
        '
        Me.txtDireccion.AutoSize = False
        Me.txtDireccion.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.txtDireccion.Location = New System.Drawing.Point(17, 192)
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(824, 36)
        Me.txtDireccion.TabIndex = 70
        Me.txtDireccion.ThemeName = "Material"
        '
        'TxtApMaterno
        '
        Me.TxtApMaterno.AutoSize = False
        Me.TxtApMaterno.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.TxtApMaterno.Location = New System.Drawing.Point(581, 62)
        Me.TxtApMaterno.Name = "TxtApMaterno"
        Me.TxtApMaterno.Size = New System.Drawing.Size(260, 36)
        Me.TxtApMaterno.TabIndex = 68
        Me.TxtApMaterno.ThemeName = "Material"
        '
        'TxtApPaterno
        '
        Me.TxtApPaterno.AutoSize = False
        Me.TxtApPaterno.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.TxtApPaterno.Location = New System.Drawing.Point(299, 62)
        Me.TxtApPaterno.Name = "TxtApPaterno"
        Me.TxtApPaterno.Size = New System.Drawing.Size(260, 36)
        Me.TxtApPaterno.TabIndex = 67
        Me.TxtApPaterno.ThemeName = "Material"
        '
        'TxtNombre
        '
        Me.TxtNombre.AutoSize = False
        Me.TxtNombre.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNombre.Location = New System.Drawing.Point(17, 62)
        Me.TxtNombre.Name = "TxtNombre"
        Me.TxtNombre.Size = New System.Drawing.Size(260, 36)
        Me.TxtNombre.TabIndex = 66
        Me.TxtNombre.Text = " "
        Me.TxtNombre.ThemeName = "Material"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label14.Location = New System.Drawing.Point(14, 296)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(71, 18)
        Me.Label14.TabIndex = 65
        Me.Label14.Text = "Folio IFE:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label13.Location = New System.Drawing.Point(296, 233)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(49, 18)
        Me.Label13.TabIndex = 64
        Me.Label13.Text = "CURP:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(14, 233)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(69, 18)
        Me.Label12.TabIndex = 63
        Me.Label12.Text = "Telefono:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(14, 171)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 18)
        Me.Label11.TabIndex = 62
        Me.Label11.Text = "Direccion:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(578, 112)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(105, 18)
        Me.Label10.TabIndex = 61
        Me.Label10.Text = "Codigo Postal:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(380, 112)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 18)
        Me.Label9.TabIndex = 60
        Me.Label9.Text = "Estado Civil:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(249, 112)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 18)
        Me.Label8.TabIndex = 59
        Me.Label8.Text = "Sexo:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(578, 231)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 18)
        Me.Label7.TabIndex = 58
        Me.Label7.Text = "RFC:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(14, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 18)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "Fecha Nacimiento:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(578, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(125, 18)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "Apellido Materno"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(296, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(122, 18)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Apellido Paterno"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(14, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 18)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "Nombre:"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox2.Controls.Add(Me.TxtImss)
        Me.RadGroupBox2.Controls.Add(Me.TxtSueldo)
        Me.RadGroupBox2.Controls.Add(Me.LstPuesto)
        Me.RadGroupBox2.Controls.Add(Me.DtFechaBajaImss)
        Me.RadGroupBox2.Controls.Add(Me.dtFechaBaja)
        Me.RadGroupBox2.Controls.Add(Me.lstNombre)
        Me.RadGroupBox2.Controls.Add(Me.DtAltaIMSS)
        Me.RadGroupBox2.Controls.Add(Me.DtIngreso)
        Me.RadGroupBox2.Controls.Add(Me.LstEmpresa)
        Me.RadGroupBox2.Controls.Add(Me.lstMatricula)
        Me.RadGroupBox2.Controls.Add(Me.Label22)
        Me.RadGroupBox2.Controls.Add(Me.Label15)
        Me.RadGroupBox2.Controls.Add(Me.Label21)
        Me.RadGroupBox2.Controls.Add(Me.Label1)
        Me.RadGroupBox2.Controls.Add(Me.Label20)
        Me.RadGroupBox2.Controls.Add(Me.Label2)
        Me.RadGroupBox2.Controls.Add(Me.Label19)
        Me.RadGroupBox2.Controls.Add(Me.Label16)
        Me.RadGroupBox2.Controls.Add(Me.Label18)
        Me.RadGroupBox2.Controls.Add(Me.Label17)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Datos Empresa"
        Me.RadGroupBox2.Location = New System.Drawing.Point(874, 380)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(839, 369)
        Me.RadGroupBox2.TabIndex = 2
        Me.RadGroupBox2.Text = "Datos Empresa"
        Me.RadGroupBox2.ThemeName = "MaterialBlueGrey"
        '
        'TxtImss
        '
        Me.TxtImss.AutoSize = False
        Me.TxtImss.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.TxtImss.Location = New System.Drawing.Point(510, 213)
        Me.TxtImss.Name = "TxtImss"
        Me.TxtImss.Size = New System.Drawing.Size(232, 36)
        Me.TxtImss.TabIndex = 98
        Me.TxtImss.ThemeName = "Material"
        '
        'TxtSueldo
        '
        Me.TxtSueldo.AutoSize = False
        Me.TxtSueldo.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.TxtSueldo.Location = New System.Drawing.Point(344, 214)
        Me.TxtSueldo.Name = "TxtSueldo"
        Me.TxtSueldo.Size = New System.Drawing.Size(153, 36)
        Me.TxtSueldo.TabIndex = 97
        Me.TxtSueldo.ThemeName = "Material"
        '
        'DtFechaBajaImss
        '
        Me.DtFechaBajaImss.AutoSize = False
        Me.DtFechaBajaImss.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.DtFechaBajaImss.Location = New System.Drawing.Point(649, 132)
        Me.DtFechaBajaImss.Mask = "00/00/0000"
        Me.DtFechaBajaImss.Name = "DtFechaBajaImss"
        Me.DtFechaBajaImss.Size = New System.Drawing.Size(174, 36)
        Me.DtFechaBajaImss.TabIndex = 93
        Me.DtFechaBajaImss.TabStop = False
        '
        'dtFechaBaja
        '
        Me.dtFechaBaja.AutoSize = False
        Me.dtFechaBaja.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.dtFechaBaja.Location = New System.Drawing.Point(468, 132)
        Me.dtFechaBaja.Mask = "00/00/0000"
        Me.dtFechaBaja.Name = "dtFechaBaja"
        Me.dtFechaBaja.Size = New System.Drawing.Size(174, 36)
        Me.dtFechaBaja.TabIndex = 92
        Me.dtFechaBaja.TabStop = False
        '
        'DtAltaIMSS
        '
        Me.DtAltaIMSS.AutoSize = False
        Me.DtAltaIMSS.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.DtAltaIMSS.Location = New System.Drawing.Point(648, 62)
        Me.DtAltaIMSS.Mask = "00/00/0000"
        Me.DtAltaIMSS.Name = "DtAltaIMSS"
        Me.DtAltaIMSS.Size = New System.Drawing.Size(175, 36)
        Me.DtAltaIMSS.TabIndex = 90
        Me.DtAltaIMSS.TabStop = False
        '
        'DtIngreso
        '
        Me.DtIngreso.AutoSize = False
        Me.DtIngreso.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.0!)
        Me.DtIngreso.Location = New System.Drawing.Point(467, 62)
        Me.DtIngreso.Mask = "00/00/0000"
        Me.DtIngreso.Name = "DtIngreso"
        Me.DtIngreso.Size = New System.Drawing.Size(175, 36)
        Me.DtIngreso.TabIndex = 89
        Me.DtIngreso.TabStop = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label22.Location = New System.Drawing.Point(507, 191)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(68, 18)
        Me.Label22.TabIndex = 86
        Me.Label22.Text = "No IMSS:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label15.Location = New System.Drawing.Point(464, 32)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(110, 18)
        Me.Label15.TabIndex = 79
        Me.Label15.Text = "Fecha Ingreso:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label21.Location = New System.Drawing.Point(341, 192)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(58, 18)
        Me.Label21.TabIndex = 85
        Me.Label21.Text = "Sueldo:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(20, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 18)
        Me.Label1.TabIndex = 78
        Me.Label1.Text = "Personal:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label20.Location = New System.Drawing.Point(135, 32)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(72, 18)
        Me.Label20.TabIndex = 84
        Me.Label20.Text = "Empresa:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(20, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 18)
        Me.Label2.TabIndex = 77
        Me.Label2.Text = "Matricula:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label19.Location = New System.Drawing.Point(20, 192)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(58, 18)
        Me.Label19.TabIndex = 83
        Me.Label19.Text = "Puesto:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label16.Location = New System.Drawing.Point(645, 32)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(124, 18)
        Me.Label16.TabIndex = 80
        Me.Label16.Text = "Fecha Alta IMSS:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label18.Location = New System.Drawing.Point(645, 112)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(129, 18)
        Me.Label18.TabIndex = 82
        Me.Label18.Text = "Fecha Baja IMSS:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label17.Location = New System.Drawing.Point(465, 112)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(91, 18)
        Me.Label17.TabIndex = 81
        Me.Label17.Text = "Fecha Baja:"
        '
        'LstPuesto
        '
        Me.LstPuesto.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstPuesto.Location = New System.Drawing.Point(23, 214)
        Me.LstPuesto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstPuesto.Name = "LstPuesto"
        Me.LstPuesto.SelectItem = ""
        Me.LstPuesto.SelectText = ""
        Me.LstPuesto.Size = New System.Drawing.Size(310, 36)
        Me.LstPuesto.TabIndex = 94
        '
        'lstNombre
        '
        Me.lstNombre.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstNombre.Location = New System.Drawing.Point(23, 134)
        Me.lstNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstNombre.Name = "lstNombre"
        Me.lstNombre.SelectItem = ""
        Me.lstNombre.SelectText = ""
        Me.lstNombre.Size = New System.Drawing.Size(436, 36)
        Me.lstNombre.TabIndex = 91
        '
        'LstEmpresa
        '
        Me.LstEmpresa.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstEmpresa.Location = New System.Drawing.Point(138, 62)
        Me.LstEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstEmpresa.Name = "LstEmpresa"
        Me.LstEmpresa.SelectItem = ""
        Me.LstEmpresa.SelectText = ""
        Me.LstEmpresa.Size = New System.Drawing.Size(321, 36)
        Me.LstEmpresa.TabIndex = 88
        '
        'lstMatricula
        '
        Me.lstMatricula.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstMatricula.Location = New System.Drawing.Point(23, 62)
        Me.lstMatricula.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstMatricula.Name = "lstMatricula"
        Me.lstMatricula.SelectItem = ""
        Me.lstMatricula.SelectText = ""
        Me.lstMatricula.Size = New System.Drawing.Size(109, 36)
        Me.lstMatricula.TabIndex = 87
        '
        'LstCp
        '
        Me.LstCp.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstCp.Location = New System.Drawing.Point(581, 134)
        Me.LstCp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstCp.Name = "LstCp"
        Me.LstCp.SelectItem = ""
        Me.LstCp.SelectText = ""
        Me.LstCp.Size = New System.Drawing.Size(260, 36)
        Me.LstCp.TabIndex = 75
        '
        'lstEdoCivil
        '
        Me.lstEdoCivil.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstEdoCivil.Location = New System.Drawing.Point(383, 134)
        Me.lstEdoCivil.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstEdoCivil.Name = "lstEdoCivil"
        Me.lstEdoCivil.SelectItem = ""
        Me.lstEdoCivil.SelectText = ""
        Me.lstEdoCivil.Size = New System.Drawing.Size(176, 36)
        Me.lstEdoCivil.TabIndex = 74
        '
        'lstSexo
        '
        Me.lstSexo.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstSexo.Location = New System.Drawing.Point(252, 134)
        Me.lstSexo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstSexo.Name = "lstSexo"
        Me.lstSexo.SelectItem = ""
        Me.lstSexo.SelectText = ""
        Me.lstSexo.Size = New System.Drawing.Size(109, 36)
        Me.lstSexo.TabIndex = 3
        '
        'TablaPersonal
        '
        Me.TablaPersonal.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TablaPersonal.CmdActualizar_enabled = True
        Me.TablaPersonal.Cmdcerrar_enabled = True
        Me.TablaPersonal.CmdEliminar_enabled = True
        Me.TablaPersonal.CmdExportaExcel_enabled = True
        Me.TablaPersonal.Cmdguardar_enabled = True
        Me.TablaPersonal.CmdNuevo_enabled = True
        Me.TablaPersonal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaPersonal.Location = New System.Drawing.Point(0, 0)
        Me.TablaPersonal.Name = "TablaPersonal"
        Me.TablaPersonal.Size = New System.Drawing.Size(1731, 362)
        Me.TablaPersonal.SqlSelect = "select"
        Me.TablaPersonal.TabIndex = 0
        Me.TablaPersonal.Tag = "P_Master"
        '
        'Control_del_Personal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(1731, 773)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Control_del_Personal"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Control del Personal"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.DTFechaNacimiento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtIfe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRfc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCurp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTelefono, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDireccion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtApMaterno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtApPaterno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNombre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.TxtImss, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSueldo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtFechaBajaImss, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaBaja, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtAltaIMSS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtIngreso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents TablaPersonal As Tabla_Filtro
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtIfe As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtRfc As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtCurp As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtTelefono As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtDireccion As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtApMaterno As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtApPaterno As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtNombre As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents DTFechaNacimiento As Telerik.WinControls.UI.RadMaskedEditBox
    Friend WithEvents LstCp As Listas
    Friend WithEvents lstEdoCivil As Listas
    Friend WithEvents lstSexo As Listas
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Label22 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents DtAltaIMSS As Telerik.WinControls.UI.RadMaskedEditBox
    Friend WithEvents DtIngreso As Telerik.WinControls.UI.RadMaskedEditBox
    Friend WithEvents LstEmpresa As Listas
    Friend WithEvents lstMatricula As Listas
    Friend WithEvents LstPuesto As Listas
    Friend WithEvents DtFechaBajaImss As Telerik.WinControls.UI.RadMaskedEditBox
    Friend WithEvents dtFechaBaja As Telerik.WinControls.UI.RadMaskedEditBox
    Friend WithEvents lstNombre As Listas
    Friend WithEvents TxtImss As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtSueldo As Telerik.WinControls.UI.RadTextBox
End Class

