   M S B A M L   `   `   `  ����%  !Flight Strip Config Tool WPF V0.1;    5Flight_Strip_Config_Tool_WPF_V0._1.ThreeColorPlusTime   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   X TXceed.Wpf.Toolkit, Version=2.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  Effekt Parameter��5   	   $	��490��6"   $	��869��6/    � WindowStartupLocation$ CenterScreen=�6;   .��5      � .+�5   
   #  Xceed.Wpf.Toolkit.ColorCanvas  -   5          Name$ color1��5      $
��Left=�6+   $��	50,80,0,0q�6F   $	��Top=�6Y   �� 5   
   -   5         ��Name$ effektLabel��5      $��Effekt :��6%   $
��Left=�68   $��	10,10,0,0q�6S   $	��Top=�6f   �� 5	   
   $��Farbe 1 des Strips :��6   $
��Left=�6/   $��
107,49,0,0q�6J   $	��Top=�6^   �� 5
   
   -   5         ��Name$ savebtn��5
      $��	Speichern��6"   $
��Left=�66   $��285,429,0,0q�6Q   $	��Top=�6f   $	��110��6~   6�   �� 5   
   -   5        $ 	cancelbtn��5      $��	Abbrechen��6$   $
��Left=�68   $��468,429,0,0q�6S   $	��Top=�6h   $	��110��6�   6�   �� 5   
   -   5         ��Name$ 
effectText��5      $
��Left=�6&   $��23��6A   $��	61,14,0,0q�6M    �� TextWrapping$
 Wrap=�6`   $��TextBox��6t   $	��Top=�6�   $	��251��6�     5   
   -   5        $ color2��5      $
��Left=�6+   $��
315,80,0,0q�6F   $	��Top=�6Z     5   
   -   5        $ color3��5      $
��Left=�6+   $��
580,80,0,0q�6F   $	��Top=�6Z   �� 5   
   $��Farbe 2 des Strips :��6   $
��Left=�6/   $��
374,49,0,0q�6J   $	��Top=�6^   �� 5   
   $��Farbe 3 des Strips :��6   $
��Left=�6/   $��
640,49,0,0q�6J   $	��Top=�6^   �� 5   
   $��Zeit bis zum Wechsel :��6   $
��Left=�61   $��
45,382,0,0q�6L   $	��Top=�6`   %  Xceed.Wpf.Toolkit.IntegerUpDown  5   
   -   5          Name$
 time��5      $
��Left=�6+   $��177,387,0,0q�6F   $	��Top=�6[   $��78��6s   �� 5   
   $��Millisekunden��6   $
��Left=�6(   $��260,382,0,0q�6C   $	��Top=�6X   5      5      