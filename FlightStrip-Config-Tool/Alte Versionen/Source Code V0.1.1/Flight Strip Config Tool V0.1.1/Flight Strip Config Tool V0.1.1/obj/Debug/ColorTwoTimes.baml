   M S B A M L   `   `   `  ����%  !Flight Strip Config Tool WPF V0.16    0Flight_Strip_Config_Tool_WPF_V0._1.ColorTwoTimes   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   X TXceed.Wpf.Toolkit, Version=2.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  Effekt Parameter��5   	   $	��526��6"   $	��337��6/    � WindowStartupLocation$ CenterScreen=�6;   .��5      � .+�5   
   #  Xceed.Wpf.Toolkit.ColorCanvas  -   5          Name$ color1��5      $
��Left=�6+   $��	45,80,0,0q�6F   $	��Top=�6Y   �� 5   
   -   5         ��Name$ effektLabel��5      $��Effekt :��6%   $
��Left=�68   $��	10,10,0,0q�6S   $	��Top=�6f   �� 5	   
   $��Farbe des Strips :��6   $
��Left=�6-   $��
108,49,0,0q�6H   $	��Top=�6\   �� 5
   
   -   5         ��Name$ savebtn��5
      $��	Speichern��6"   $
��Left=�66   $��
14,459,0,0q�6Q   $	��Top=�6e   $	��110��6}   6�   �� 5   
   -   5        $ 	cancelbtn��5      $��	Abbrechen��6$   $
��Left=�68   $��206,459,0,0q�6S   $	��Top=�6h   $	��110��6�   6�   �� 5   
   -   5         ��Name$ 
effectText��5      $
��Left=�6&   $��23��6A   $��	61,14,0,0q�6M    �� TextWrapping$
 Wrap=�6`   $��TextBox��6t   $	��Top=�6�   $	��262��6�   �� 5   
   $��Zeit die Strip an ist :��6   $
��Left=�62   $��
14,382,0,0q�6M   $	��Top=�6a   %  Xceed.Wpf.Toolkit.IntegerUpDown  5   
   -   5          Name$ time1��5      $
��Left=�6,   $��137,386,0,0q�6G   $	��Top=�6\   $��78��6t   �� 5   
   $��Millisekunden��6   $
��Left=�6(   $��220,382,0,0q�6C   $	��Top=�6X   �� 5   
   $��Zeit die Strip aus ist :��6   $
��Left=�63   $��
14,411,0,0q�6N   $	��Top=�6b     5   
   -   5        $ time2��5      $
��Left=�6,   $��137,415,0,0q�6G   $	��Top=�6\   $��78��6t   �� 5   
   $��Millisekunden��6   $
��Left=�6(   $��220,411,0,0q�6C   $	��Top=�6X   5      5      