   M S B A M L   `   `   `  ����V  RwpfPortfolioAssetManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:wpfPortfolioAssetManagementwpfPortfolioAssetManagement  5   	   -    'wpfPortfolioAssetManagement.AssetSearch   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:wpfPortfolioAssetManagement     5   	     � Title$  Search Asset Symbol��5   	   $	��470��6%   $	��824��62   .��5	      ]� .+�5
   
   � .+�5      �� $��10,50,10,10q�6    �� Orientation$ Vertical=�6.   $	��450��6E   $	��800��6R   .+�5      �� -   5         ��Name$ lvAssets��5      $
��Left=�6-   $	��375��6H   $	��Top=�6U   $	��795��6m    � ContextMenu 5      �� 5      .@�5      m� -   5        $��Menu_WatchlistAdd��5   '   $��Add to Watchlist��6`   5       �� View 5      �� 5      .P�5      �� $��Symbol��6-    �� Width$ 80��6e    �� DisplayMemberBinding 6=   �� *Symbol+6e   �� 5      $
��Name��6-   $	 200��6a    6;   �� *Name+6a   �� 5      $��Exchange��6-   $ 80��6i    6?   �� *
Exchange+6i   �� 5      $
��Type��6-   $ 80��6a    6;   �� *Type+6a   �� 5      $��ExchangeDisp��6-   $ 80��6q    6C   �� *ExchangeDisp+6q   �� 5      $��TypeDisp��6-   $ 80��6i    6?   �� *
TypeDisp+6i   5      5      5       �� 5!      -   5         ��Name$ tbSearch��5!      $
��Left=�6=   $��23��6X   $��	10,15,0,0q�6d    �� TextWrapping$
 Wrap=�6w   $�� ��6�   $	��Top=�6�   $	��420��6�   �� 5"      -   5        	 ��Name$	 label��5"      $��Lang:��6#   $
��Left=�63   $��28��6N   $��
435,10,0,0q�6Z   $	��Top=�6n   $��41��6�   �� 5$      -   5        
 ��Name$
 cmbLang��5$      $
��Left=�6(   $��20��6C   $��
477,15,0,0q�6O   $	��Top=�6c   $��45��6{   $��8,2,5,3q�6�   .@�5%      ��  �� 
IsSelected . $��en��61   �� 5&      $��fr��6   �� 5'      $��de��6   �� 5(      $��it��6   �� 5)      $��zh��6   5*      �� 5+      -   5        $	 	lblRegion��5+      $��Region:��6'   $
��Left=�69   $��28��6T   $��
527,10,0,0q�6`   $	��Top=�6t   $��55��6�   �� 5,      -   5        $
 	cmbRegion��5,      $
��Left=�6*   $��20��6E   $��
583,15,0,0q�6Q   $	��Top=�6e   $��45��6}   $��8,2,5,3q�6�   .@�5-      ��  . $��US��61   �� 5.      $��AU��6   �� 5/      $��CA��6   �� 50      $��DE��6   �� 51      $��ES��6   �� 52      $��FR��6   �� 53      $��GB��6   �� 54      $��HK��6   �� 55      $��IN��6   �� 56      $��IT��6   57      �� 58      -   5        $	 lblRegion_Copy��58      $��Source:��6,   $
��Left=�6>   $��28��6Y   $��
633,10,0,0q�6e   $	��Top=�6y   $��55��6�   �� 59      -	   5        $
 comboBox��59      $
��Left=�6)   $��26��6D   $��
689,12,0,0q�6P   $	��Top=�6d   $	��117��6|   .@�5:      ��  . $��Yahoo Finance��61   5;      5=      5>      5@      