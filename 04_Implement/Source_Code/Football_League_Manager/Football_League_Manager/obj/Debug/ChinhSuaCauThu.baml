   M S B A M L   `   `   `  ����R  NFootball_League_Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Football_League_ManagerFootball_League_Manager  5   	   ,    &Football_League_Manager.ChinhSuaCauThu   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   1local%clr-namespace:Football_League_Manager  5   	   -   5          � Title$  ChinhSuaCauThu��5   	   $��536.424��6    $
��1091��61   .��5      �� $��0,-2,0,2q�6   .+�5   
   �� $��Tên cầu thủ:��6    �� Left$ 24��6)   $	��134��6;   
 �� Top$ 38��6G   �� 5   
   -   5        $��TenCauThuText��5      $	��212��6(   $��38��64   $ 98��6@   $ 20��6Q   6a   &   System.Windows.Controls.DataGrid  5   
   -   5        $��CauThuDataGrid��5         AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .  $	��460��5      $
��Left=�5      $��Center=�5         
IsReadOnly . $��Hidden=�5      $	��730��5      $ 20��6      AlternatingRowBackground
 �����$	 334��6P   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 200 6H      CanUserResize .     Header$ Tên cầu thủ��6j   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$ 	TenCauThu�6j     5      $	
 150 6M    .  $ 
Ngày sinh��6o    6%   �� $ NgaySinhCauThu�6o     5      $	
 100 6C    .  $ Số áo��6e    6%   �� $
 SoAo�6e     5      $	
 150 6I    .  $ Quốc tịch��6k    6%   �� $ 
QuocTichCT�6k     5      $	
 200 6D    .  $ 	Vị trí��6f    6%   �� $ ViTri�6f   5       �� 5"   
   $��Ngày sinh ��6   $ 24��6&   $	��134��68   $	 120��6D   �� 5#   
   $��
Số áo: ��6   $ 24��6#   $	��134��65   $	 206��6A   �� 5$   
   -   5         ��Name$ SoAoText��5$      $	��212��6$   $��38��60   $ 98��6<   $	 186��6M   6^   �� 5%   
   $��Vị trí: ��6   $ 24��6$   $	��134��66   $	 382��6B   �� 5'   
   $��Quốc tịch: ��6   $ 24��6'   $	��134��69   $	 298��6E   �� 5(   
   -   5        $ QuocTichText��5(      $	��212��6(   $��38��64   $ 98��6@   $	 277��6Q   (  "System.Windows.Controls.DatePicker  5*   
   -   5        $��NgaySinhDatePicker��5*   !   $	��212��6    (  $MaterialDesignFloatingHintDatePicker#��[  6;   $ 98��6y   $	 101��6�   6�   �� 5+   
   -   5        $��itemsCombobox��5+      $	��212��6(   $��30��64   $ 98��6@   $	 370��6Q   ��5,      �� 5-      .R�5.      � .+�50      �� $
��Bold"�6<   ��6$   �� *TenVT+6P   52      53      55      �� 56   
   -   5         ��Name$ 
ThemButton��56       $��Thêm��6   $��91��64   $��30��6?   $	 114��6K   $	 450��6]   6n   �� 57   
   -	   5        $ 	XoaButton��57   3   $
��Xoá��6   $y�Hidden=�6   $��91��6F   $��30��6Q   $	 114��6]   $	 450��6o   6�   �� 58   
   -
   5        $ 	SuaButton��58   3   $��Sửa��6   $y�Hidden=�6   $��91��6F   $��30��6Q   $ 10��6]   $	 450��6n   6   �� 59   
   -   5        $ HoanTatButton��59   $   $��Kết thúc��6   $��92��6;   $��30��6F   $	 218��6R   $	 450��6d   �� 5:   
   -   5        $ HuyBoButton��5:   P   $��
Huỷ bỏ��6   $y�Hidden=�6"   $��92��6e   $��30��6p   $	 218��6|   $	 450��6�   5<      5=      