   M S B A M L   `   `   `  ����  Port    Port.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Port Management��5   	   $	��450��6!   $	��800��6.   ���	#FF2D2F45 � 	Resources 5       �� 
TargetType �� Property �� Value   TransparentScrollBar �� 
TargetType �Name � RowDefinitions ]�Name	 ]� Thumb
 n�Name ]� DecreaseRepeatButton ]� IncreaseRepeatButton��%Z  '��       '�� k     &	  v    �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   �� 5       � ��$ 5q�6'   5	      �� 5
   
   " ��6   .�5      ��  � ��$ 14(�6)   �� 5       � ��$ 5q�6'   �� 5       � ��$
 10,5q�6(   �� 5       � �� �#007ACC6+   �� 5       � ��
 �����6+   �� 5       � �� �#005A9E6,   5      �� 5   
   " ��6-   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � $
 Grid��6    5      �� 5   "   $��* �60   �� 5   "   $
��Auto �60   �� 5   "   $��* �60   .+�5      ]� $ 
PART_Track��6$   $��1��68   ��. 	 5   "   n� 5    &   $
 Thumb��6,   
������ 6;    5"   "   �� 5#   &   $��ScrollBar.LineUpCommand��63   
������ 
������ ��6U   Z� 5$   H    5&   "   �� 5'   &   $��ScrollBar.LineDownCommand��63   
������ 
������ ��6W   Z� 5(   H   5*      5+      5,      5.      �� 5/       � ��
 ���� 6+   �� 50       � ��
 ���� 6+   �� 51       � ��
 ���� 6,   52      54      .��56      ]� .+�58   
   �� $	��Top=�6    �� Orientation$ 
Horizontal=�6*   ���	#FF28293D$��10q�6Z   .+�59      �� $!��Портты еңгіз →��6   $��
Nirmala UI)�6-   $
��Bold"�6E   $��	20,0,10,0q�6W   $��Center=�6j   
�������$��18(�6�   �� 5:      -   5         ��Name$ portTextBox��5:      $	��100��6+   $��	10,0,10,0q�67   $��Center=�6J    � ToolTip$ Введите порт��6e   �� 5;      -   5        $��Портты ашу��5;      $��0,5,5,5q�6A   $��
Nirmala UI)�6i   $
��Bold"�6�   $��20,10,20,10q�6�   ��6R   Z� ��5<      �� 5=       �� EndPoint$ 0.5,1&�6*    �� 
StartPoint$ 0.5,0&�6;   .��5>      �  � Color$ 	#FF5561FF��6'   � 5?      $ 	#FF3643FC��6'    � Offset$ 1Q�69   5@      5B      �� 5D      -   5        $��Портты жабу��5D      $��0,5,5,5q�6C   $��
Nirmala UI)�6k   $
��Bold"�6�   $��20,10,20,10q�6�   ��6T   Z� ��5E      �� 5F      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5G      � $ 	#FF5561FF��6'   � 5H      $ 	#FF3643FC��6'   $ 1Q�69   5I      5K      �� 5M      -   5        $��Жаңарту��5M      $��0,5,5,5q�6=   $��
Nirmala UI)�6e   $
��Bold"�6}   $��20,10,20,10q�6�   ��6N   Z� ��5N      �� 5O      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5P      � $ 	#FF5561FF��6'   � 5Q      $ 	#FF3643FC��6'   $ 1Q�69   5R      5T      �� 5U      $	��107��6   .��5V      �� $3��-by Қанат Аружан Қанатқызы��6   $
��Wrap=�6=   
�������6Q   5W      5X      �� 5[   
   $
��Auto=�6   
������  5\       �� BasedOn��%0   '��       7   �� 5]      " ��6   8   6/   5^      .��5_      � $��10q�6    � ColumnDefinitions 5`      �� 5a      $��2* �6'   �� 5b      $��2* �6'   �� 5c      $��2* �6'    5e      �� 5f      $
��Auto �6$   .+�5i      �� -   5        $ openPortsTextBox��5i      $"��Открытые порты:��64   $��0��6K   $	��Top=�6[    �� 
IsReadOnly .  �� TextWrapping$
 Wrap=�6�   " �� VerticalScrollBarVisibility$
 Auto=�6�   ���	#FF242537
�������6�   �� 5j      -   5        $ closedPortsTextBox��5j      $"��Закрытые порты:��66   $��1��6M   $	��Top=�6]    . $
 Wrap=�6�   $
 Auto=�6�   ���	#FF242537
�������6�   �� 5k      -   5        $ noOptionPortsTextBox��5k      $#��Порты без опций:��68   $��2��6P   $	��Top=�6`    . $
 Wrap=�6�   $
 Auto=�6�   ���	#FF242537
�������6�   5l      5m      5n      5o      