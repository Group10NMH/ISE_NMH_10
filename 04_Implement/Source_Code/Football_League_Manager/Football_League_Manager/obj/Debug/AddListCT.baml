   M S B A M L   `   `   `  ����R  NFootball_League_Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Football_League_ManagerFootball_League_Manager  5   	   '    !Football_League_Manager.AddListCT   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      1local%clr-namespace:Football_League_Manager  5   	     � Title$  Danh sách cầu thủ��5
   	   $	��450��6#   $
��1000��60   .��5      �� .+�5   
   �� -   5         ��Name$
 name��5   �   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tên cầu thủ��6    %  !MaterialDesignFloatingHintTextBox#��[  6@    �� Left$ 22��6{   
 �� Top$ 20��6�   $	��240��6�   6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ NgaySinh��5   n   $ 
Ngày sinh��5       ( $MaterialDesignFloatingHintDatePicker#��[ 5      $ 22��6A   $ 85��6R   $	��240��6b   6n   �� 5   
   -   5        $ QuocTich��5   �   $ Quốc tịch��6   #��[  6>   $ 22��6y   $	 150��6�   $	��240��6�   6�   �� 5   
   -   5        $ Number��5   �   $ Số áo��6   #��[  6;   $ 22��6v   $	 215��6�   $	��240��6�   6�   �� 5   
   -   5        $��ViTri��5   ?   $ 	Vị trí��5       & "MaterialDesignFloatingHintComboBox#��[ 5      $	��240��6L   $ 22��6q   $	 280��6�   �� 5   
   -   5        $��DeleteButton��5      $��Xóa cầu thủ��6'   $ 40��6=   $	 360��6N   $	��187��6_   $��40��6k   
�����_�$y�Hidden=�6�   �� 5   
   -   5        $��UpdateButton��5      $��Sửa cầu thủ��6%   $	 285��6;   $	 360��6M   $	��187��6^   $��40��6j   
�����_�$y�Hidden=�6�   �� 5   
   -   5        $��Thêm cầu thủ��5      $	 530��6(   $	 360��6:   $	��187��6K   $��40��6W   
�����_�6z   �� 5   
   -	   5        $��Hoàn tất��5      $	 775��6$   $	 360��66   $	��187��6G   $��40��6S   
�����_�6v   &   System.Windows.Controls.DataGrid  5   
   -
   5        $��CauThuDataGrid��5         AutoGenerateColumns .     
IsReadOnly . $	��313��5      $
��Left=�5      $��Center=�5      $	��715��5      $	 269��6   $ 11��6)      AlternatingRowBackground
 �����$��Disabled=�5      	   Columns	 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 55 6B      CanUserResize .     Header$	 STT��6c   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$	 STT�6c     5#      $	
 200 6D    .  $ Tên cầu thủ��6f    6%   �� $ TenCT�6f     5$      $	
 115 6I    .  $ Quốc Tịch��6k    6%   �� $ 
QuocTichCT�6k     5%      $	
 115 6I    .  $ 
Ngày sinh��6k    6%   �� $ 
NgaySinhCT�6k     5&      $	
 115 6F    .  $ 	Vị trí��6h    6%   �� $ ViTriCT�6h     5'      $	
 115 6E    .  $ Số áo��6g    6%   �� $ SoAoCT�6g   5)      5+      5,      