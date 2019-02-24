﻿using UnityEngine;

using ColossalFramework.UI;
using System.Collections.Generic;
using UIUtils = SamsamTS.UIUtils;

namespace MoveIt
{
    public class UIToolOptionPanel : UIPanel
    {
        public static UIToolOptionPanel instance;

        private UIButton m_group;
        private UIButton m_save;
        private UIButton m_load;

        private UIMultiStateButton m_followTerrain;
        private UIMultiStateButton m_snapping;

        private UITabstrip m_tabStrip;
        private UIButton m_single;
        private UIButton m_marquee;
        private UIButton m_copy;
        private UIButton m_bulldoze;
        private UIButton m_alignTools;

        public UIMultiStateButton PO_button;
        public UIMultiStateButton grid;
        public UIMultiStateButton underground;

        public UIPanel filtersPanel;
        public UIPanel alignToolsPanel;
        public UIPanel viewOptions;

        public override void Start()
        {
            instance = this;

            atlas = UIUtils.GetAtlas("Ingame");
            size = new Vector2(41, 41);
            relativePosition = new Vector2(GetUIView().GetScreenResolution().x - 448, -41);
            name = "MoveIt_ToolOptionPanel";

            DebugUtils.Log("ToolOptionPanel position: " + absolutePosition);

            #region Group
            // Group
            m_group = AddUIComponent<UIButton>();
            m_group.name = "MoveIt_Group";
            m_group.group = m_tabStrip;
            m_group.atlas = GetIconsAtlas();
            m_group.tooltip = "Group";
            m_group.playAudioEvents = true;

            m_group.size = new Vector2(36, 36);

            m_group.normalBgSprite = "OptionBase";
            m_group.hoveredBgSprite = "OptionBaseHovered";
            m_group.pressedBgSprite = "OptionBasePressed";
            m_group.disabledBgSprite = "OptionBaseDisabled";

            m_group.normalFgSprite = "Group";

            m_group.relativePosition = Vector2.zero;
            m_group.isVisible = false; //TODO: temporary
            #endregion

            #region Save
            m_save = AddUIComponent<UIButton>();
            m_save.name = "MoveIt_Save";
            m_save.group = m_tabStrip;
            m_save.atlas = GetIconsAtlas();
            m_save.tooltip = "Export";
            m_save.playAudioEvents = true;

            m_save.size = new Vector2(36, 36);

            m_save.normalBgSprite = "OptionBase";
            m_save.hoveredBgSprite = "OptionBaseHovered";
            m_save.pressedBgSprite = "OptionBasePressed";
            m_save.disabledBgSprite = "OptionBaseDisabled";

            m_save.normalFgSprite = "Save";
            m_save.disabledFgSprite = "Save_disabled";

            m_save.relativePosition = m_group.relativePosition + new Vector3(m_group.width, 0);

            m_save.eventClicked += (c, p) =>
            {
                if (MoveItTool.IsExportSelectionValid())
                {
                    UISaveWindow.Open();
                }
                else
                {
                    UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage("Selection invalid", "The selection is empty or invalid.", false);
                }
            };
            #endregion

            #region Load
            m_load = AddUIComponent<UIButton>();
            m_load.name = "MoveIt_Load";
            m_load.group = m_tabStrip;
            m_load.atlas = GetIconsAtlas();
            m_load.tooltip = "Import";
            m_load.playAudioEvents = true;

            m_load.size = new Vector2(36, 36);

            m_load.normalBgSprite = "OptionBase";
            m_load.hoveredBgSprite = "OptionBaseHovered";
            m_load.pressedBgSprite = "OptionBasePressed";
            m_load.disabledBgSprite = "OptionBaseDisabled";

            m_load.normalFgSprite = "Load";

            m_load.relativePosition = m_save.relativePosition + new Vector3(m_save.width, 0);

            m_load.eventClicked += (c, p) =>
            {
                UILoadWindow.Open();
            };
            #endregion

            #region Follow Terrain
            m_followTerrain = AddUIComponent<UIMultiStateButton>();
            m_followTerrain.atlas = GetFollowTerrainAtlas();
            m_followTerrain.name = "MoveIt_FollowTerrain";
            m_followTerrain.tooltip = "Follow Terrain";
            m_followTerrain.playAudioEvents = true;

            m_followTerrain.size = new Vector2(36, 36);
            m_followTerrain.spritePadding = new RectOffset();

            m_followTerrain.backgroundSprites[0].disabled = "ToggleBaseDisabled";
            m_followTerrain.backgroundSprites[0].hovered = "ToggleBaseHovered";
            m_followTerrain.backgroundSprites[0].normal = "ToggleBase";
            m_followTerrain.backgroundSprites[0].pressed = "ToggleBasePressed";

            m_followTerrain.backgroundSprites.AddState();
            m_followTerrain.backgroundSprites[1].disabled = "ToggleBaseDisabled";
            m_followTerrain.backgroundSprites[1].hovered = "";
            m_followTerrain.backgroundSprites[1].normal = "ToggleBaseFocused";
            m_followTerrain.backgroundSprites[1].pressed = "ToggleBasePressed";

            m_followTerrain.foregroundSprites[0].normal = "FollowTerrain_disabled";

            m_followTerrain.foregroundSprites.AddState();
            m_followTerrain.foregroundSprites[1].normal = "FollowTerrain";

            m_followTerrain.relativePosition = m_load.relativePosition + new Vector3(m_load.width + m_load.width / 2, 0);

            m_followTerrain.activeStateIndex = MoveItTool.followTerrain ? 1 : 0;

            m_followTerrain.eventClicked += (c, p) =>
            {
                MoveItTool.followTerrain = (m_followTerrain.activeStateIndex == 1);
                MoveItTool.followTerrainModeEnabled.value = (m_followTerrain.activeStateIndex == 1);
            };
            #endregion

            #region Snapping
            m_snapping = AddUIComponent<UIMultiStateButton>();
            m_snapping.atlas = UIUtils.GetAtlas("Ingame");
            m_snapping.name = "MoveIt_Snapping";
            m_snapping.tooltip = "Toggle Snapping";
            m_snapping.playAudioEvents = true;

            m_snapping.size = new Vector2(36, 36);
            m_snapping.spritePadding = new RectOffset();

            m_snapping.backgroundSprites[0].disabled = "ToggleBaseDisabled";
            m_snapping.backgroundSprites[0].hovered = "ToggleBaseHovered";
            m_snapping.backgroundSprites[0].normal = "ToggleBase";
            m_snapping.backgroundSprites[0].pressed = "ToggleBasePressed";

            m_snapping.backgroundSprites.AddState();
            m_snapping.backgroundSprites[1].disabled = "ToggleBaseDisabled";
            m_snapping.backgroundSprites[1].hovered = "";
            m_snapping.backgroundSprites[1].normal = "ToggleBaseFocused";
            m_snapping.backgroundSprites[1].pressed = "ToggleBasePressed";

            m_snapping.foregroundSprites[0].disabled = "SnappingDisabled";
            m_snapping.foregroundSprites[0].hovered = "SnappingHovered";
            m_snapping.foregroundSprites[0].normal = "Snapping";
            m_snapping.foregroundSprites[0].pressed = "SnappingPressed";

            m_snapping.foregroundSprites.AddState();
            m_snapping.foregroundSprites[1].disabled = "SnappingDisabled";
            m_snapping.foregroundSprites[1].hovered = "";
            m_snapping.foregroundSprites[1].normal = "SnappingFocused";
            m_snapping.foregroundSprites[1].pressed = "SnappingPressed";

            m_snapping.relativePosition = m_followTerrain.relativePosition + new Vector3(m_followTerrain.width, 0);

            m_snapping.activeStateIndex = (MoveItTool.instance != null && MoveItTool.instance.snapping) ? 1 : 0;

            m_snapping.eventClicked += (c, p) =>
            {
                if (MoveItTool.instance != null)
                {
                    MoveItTool.instance.snapping = (m_snapping.activeStateIndex == 1);
                }
            };
            #endregion

            m_tabStrip = AddUIComponent<UITabstrip>();
            m_tabStrip.size = new Vector2(36, 72);

            m_tabStrip.relativePosition = m_snapping.relativePosition + new Vector3(m_snapping.width, 0);

            #region Single Select
            m_single = m_tabStrip.AddTab("MoveIt_Single", null, false);
            m_single.group = m_tabStrip;
            m_single.atlas = UIUtils.GetAtlas("Ingame");
            m_single.tooltip = "Single Selection";
            m_single.playAudioEvents = true;

            m_single.size = new Vector2(36, 36);

            m_single.normalBgSprite = "OptionBase";
            m_single.focusedBgSprite = "OptionBaseFocused";
            m_single.hoveredBgSprite = "OptionBaseHovered";
            m_single.pressedBgSprite = "OptionBasePressed";
            m_single.disabledBgSprite = "OptionBaseDisabled";
            m_single.text = "•";
            m_single.textScale = 1.5f;
            m_single.textPadding = new RectOffset(0, 1, 4, 0);
            m_single.textColor = new Color32(119, 124, 126, 255);
            m_single.hoveredTextColor = new Color32(110, 113, 114, 255);
            m_single.pressedTextColor = new Color32(172, 175, 176, 255);
            m_single.focusedTextColor = new Color32(187, 224, 235, 255);
            m_single.disabledTextColor = new Color32(66, 69, 70, 255);
            #endregion

            #region Marquee Select
            m_marquee = m_tabStrip.AddTab("MoveIt_Marquee", null, false);
            m_marquee.group = m_tabStrip;
            m_marquee.atlas = UIUtils.GetAtlas("Ingame");
            m_marquee.tooltip = "Marquee Selection";
            m_marquee.playAudioEvents = true;

            m_marquee.size = new Vector2(36, 36);

            m_marquee.normalBgSprite = "OptionBase";
            m_marquee.focusedBgSprite = "OptionBaseFocused";
            m_marquee.hoveredBgSprite = "OptionBaseHovered";
            m_marquee.pressedBgSprite = "OptionBasePressed";
            m_marquee.disabledBgSprite = "OptionBaseDisabled";

            m_marquee.normalFgSprite = "ZoningOptionMarquee";
            m_marquee.relativePosition = m_single.relativePosition + new Vector3(m_single.width, 0);
            #endregion

            #region filtersPanel
            filtersPanel = AddUIComponent(typeof(UIPanel)) as UIPanel;
            filtersPanel.atlas = UIUtils.GetAtlas("Ingame");
            filtersPanel.backgroundSprite = "SubcategoriesPanel";
            filtersPanel.clipChildren = true;

            filtersPanel.size = new Vector2(150, 140);
            filtersPanel.isVisible = false;
            UIFilters.FilterPanel = filtersPanel;

            void OnDoubleClick(UIComponent c, UIMouseEventParameter p)
            {
                foreach (UICheckBox cb in UIFilters.FilterCBs)
                {
                    cb.isChecked = false;
                    Filters.SetFilter(cb.name, false);
                }
                ((UICheckBox)c).isChecked = true;
                Filters.SetFilter(c.name, true);

                UIFilters.RefreshFilters();
            }

            #region Standard Filters
            UICheckBox checkBox = UIFilters.CreateFilterCB(filtersPanel, "Buildings");
            checkBox.eventDoubleClick += OnDoubleClick;

            checkBox = UIFilters.CreateFilterCB(filtersPanel, "Props");
            checkBox.eventDoubleClick += OnDoubleClick;

            checkBox = UIFilters.CreateFilterCB(filtersPanel, "Decals");
            checkBox.eventDoubleClick += OnDoubleClick;

            checkBox = UIFilters.CreateFilterCB(filtersPanel, "Surfaces");
            checkBox.eventDoubleClick += OnDoubleClick;

            checkBox = UIFilters.CreateFilterCB(filtersPanel, "Trees");
            checkBox.eventDoubleClick += OnDoubleClick;

            if (MoveItTool.PO.Enabled)
            {
                if (MoveItTool.PO.Active)
                {
                    filtersPanel.height += 20f;
                }
                checkBox = UIFilters.CreateFilterCB(filtersPanel, "PO");
                checkBox.eventDoubleClick += OnDoubleClick;
                checkBox.isVisible = MoveItTool.PO.Active;
            }

            checkBox = UIFilters.CreateFilterCB(filtersPanel, "Nodes");
            checkBox.eventDoubleClick += OnDoubleClick;

            checkBox = UIFilters.CreateFilterCB(filtersPanel, "Segments");
            checkBox.eventDoubleClick += OnDoubleClick;
            #endregion


            #region Network Filters
            UIButton btnNetworks = UIFilters.CreateToggleNFBtn();
            void OnDoubleClickNetworkFilter(UIComponent c, UIMouseEventParameter p)
            {
                foreach (UICheckBox cb in UIFilters.NetworkCBs)
                {
                    cb.isChecked = false;
                    Filters.SetNetworkFilter(cb.name, false);
                }
                ((UICheckBox)c).isChecked = true;
                Filters.SetNetworkFilter(c.name, true);

                UIFilters.RefreshFilters();
            }

            checkBox = UIFilters.CreateNetworkFilterCB(filtersPanel, "Roads");
            checkBox.eventDoubleClick += OnDoubleClickNetworkFilter;

            checkBox = UIFilters.CreateNetworkFilterCB(filtersPanel, "Tracks");
            checkBox.eventDoubleClick += OnDoubleClickNetworkFilter;

            checkBox = UIFilters.CreateNetworkFilterCB(filtersPanel, "Paths");
            checkBox.eventDoubleClick += OnDoubleClickNetworkFilter;

            checkBox = UIFilters.CreateNetworkFilterCB(filtersPanel, "Fences");
            checkBox.eventDoubleClick += OnDoubleClickNetworkFilter;

            checkBox = UIFilters.CreateNetworkFilterCB(filtersPanel, "Powerlines");
            checkBox.eventDoubleClick += OnDoubleClickNetworkFilter;

            checkBox = UIFilters.CreateNetworkFilterCB(filtersPanel, "Others");
            checkBox.eventDoubleClick += OnDoubleClickNetworkFilter;

            UIFilters.RefreshFilters();
            #endregion


            filtersPanel.padding = new RectOffset(10, 10, 10, 10);
            filtersPanel.autoLayoutDirection = LayoutDirection.Vertical;
            filtersPanel.autoLayoutPadding = new RectOffset(0, 0, 0, 5);
            filtersPanel.autoLayout = true;
            filtersPanel.height = 210;
            filtersPanel.absolutePosition = m_marquee.absolutePosition + new Vector3(-47, -5 - filtersPanel.height);
            #endregion

            m_marquee.eventButtonStateChanged += (c, p) =>
            {
                MoveItTool.marqueeSelection = p == UIButton.ButtonState.Focused;
                filtersPanel.isVisible = MoveItTool.marqueeSelection;

                if (UITipsWindow.instance != null)
                {
                    UITipsWindow.instance.RefreshPosition();
                }
            };
            m_marquee.eventDoubleClick += (UIComponent c, UIMouseEventParameter p) =>
            {
                bool newChecked = false;
                foreach (UICheckBox cb in filtersPanel.GetComponentsInChildren<UICheckBox>())
                {
                    if (!cb.isChecked)
                    {
                        newChecked = true;
                        break;
                    }
                }

                foreach (UICheckBox cb in filtersPanel.GetComponentsInChildren<UICheckBox>())
                {
                    cb.isChecked = newChecked;
                    Filters.SetAnyFilter(cb.label.text, newChecked);
                }
            };

            #region Copy
            m_copy = AddUIComponent<UIButton>();
            m_copy.name = "MoveIt_Copy";
            m_copy.group = m_tabStrip;
            m_copy.atlas = GetIconsAtlas();
            m_copy.tooltip = "Copy";
            m_copy.playAudioEvents = true;

            m_copy.size = new Vector2(36, 36);

            m_copy.normalBgSprite = "OptionBase";
            m_copy.hoveredBgSprite = "OptionBaseHovered";
            m_copy.pressedBgSprite = "OptionBasePressed";
            m_copy.disabledBgSprite = "OptionBaseDisabled";

            m_copy.normalFgSprite = "Copy";

            m_copy.relativePosition = m_tabStrip.relativePosition + new Vector3(m_single.width + m_marquee.width, 0); 

            m_copy.eventClicked += (c, p) =>
            {
                if (MoveItTool.instance != null)
                {
                    if (MoveItTool.instance.ToolState == MoveItTool.ToolStates.Cloning)
                    {
                        MoveItTool.instance.StopCloning();
                    }
                    else
                    {
                        MoveItTool.instance.StartCloning();
                    }
                }
            };
            #endregion

            #region Bulldoze
            m_bulldoze = AddUIComponent<UIButton>();
            m_bulldoze.name = "MoveIt_Bulldoze";
            m_bulldoze.group = m_tabStrip;
            m_bulldoze.atlas = GetIconsAtlas();
            m_bulldoze.tooltip = "Bulldoze";
            m_bulldoze.playAudioEvents = true;

            m_bulldoze.size = new Vector2(36, 36);

            m_bulldoze.normalBgSprite = "OptionBase";
            m_bulldoze.hoveredBgSprite = "OptionBaseHovered";
            m_bulldoze.pressedBgSprite = "OptionBasePressed";
            m_bulldoze.disabledBgSprite = "OptionBaseDisabled";

            m_bulldoze.normalFgSprite = "Bulldoze";

            m_bulldoze.relativePosition = m_copy.relativePosition + new Vector3(m_copy.width, 0);

            m_bulldoze.eventClicked += (c, p) =>
            {
                if (MoveItTool.instance != null)
                {
                    MoveItTool.instance.StartBulldoze();
                }
            };
            #endregion

            #region Align Tools
            m_alignTools = AddUIComponent<UIButton>();
            UIAlignTools.AlignToolsBtn = m_alignTools;
            m_alignTools.name = "MoveIt_AlignToolsBtn";
            m_alignTools.group = m_tabStrip;
            m_alignTools.atlas = GetIconsAtlas();
            m_alignTools.tooltip = "Alignment Tools";
            m_alignTools.playAudioEvents = true;
            m_alignTools.size = new Vector2(36, 36);
            m_alignTools.normalBgSprite = "OptionBase";
            m_alignTools.hoveredBgSprite = "OptionBaseHovered";
            m_alignTools.pressedBgSprite = "OptionBasePressed";
            m_alignTools.disabledBgSprite = "OptionBaseDisabled";
            m_alignTools.normalFgSprite = "AlignTools";
            m_alignTools.relativePosition = m_bulldoze.relativePosition + new Vector3(m_bulldoze.width, 0);
            m_alignTools.eventClicked += UIAlignTools.AlignToolsClicked;

            alignToolsPanel = AddUIComponent<UIPanel>();
            UIAlignTools.AlignToolsPanel = alignToolsPanel;
            alignToolsPanel.autoLayout = false;
            alignToolsPanel.clipChildren = true;
            alignToolsPanel.size = new Vector2(36, 242); // Previous:238
            alignToolsPanel.isVisible = false;
            alignToolsPanel.absolutePosition = m_alignTools.absolutePosition + new Vector3(0, 10 - alignToolsPanel.height);
            m_alignTools.zOrder = alignToolsPanel.zOrder + 10;

            UIPanel atpBackground = alignToolsPanel.AddUIComponent<UIPanel>();
            atpBackground.size = new Vector2(26, 236);
            atpBackground.clipChildren = true;
            atpBackground.relativePosition = new Vector3(5, 10);
            atpBackground.atlas = UIUtils.GetAtlas("Ingame");
            atpBackground.backgroundSprite = "InfoPanelBack";

            UIPanel atpContainer = alignToolsPanel.AddUIComponent<UIPanel>();
            atpContainer.autoLayoutDirection = LayoutDirection.Vertical;
            atpContainer.autoLayoutPadding = new RectOffset(0, 0, 0, 3);
            atpContainer.autoLayout = true;
            atpContainer.relativePosition = Vector3.zero;

            UIAlignTools.AlignButtons.Add("MoveIt_AlignRandomBtn", atpContainer.AddUIComponent<UIButton>());
            UIButton alignRandom = UIAlignTools.AlignButtons["MoveIt_AlignRandomBtn"];
            alignRandom.name = "MoveIt_AlignRandomBtn";
            alignRandom.atlas = GetIconsAtlas();
            alignRandom.tooltip = "Immediate rotate valid items randomly";
            alignRandom.playAudioEvents = true;
            alignRandom.size = new Vector2(36, 36);
            alignRandom.normalBgSprite = "OptionBase";
            alignRandom.hoveredBgSprite = "OptionBaseHovered";
            alignRandom.pressedBgSprite = "OptionBasePressed";
            alignRandom.disabledBgSprite = "OptionBaseDisabled";
            alignRandom.normalFgSprite = "AlignRandom";
            alignRandom.eventClicked += UIAlignTools.AlignToolsClicked;

            UIAlignTools.AlignButtons.Add("MoveIt_AlignGroupBtn", atpContainer.AddUIComponent<UIButton>());
            UIButton alignGroup = UIAlignTools.AlignButtons["MoveIt_AlignGroupBtn"];
            alignGroup.name = "MoveIt_AlignGroupBtn";
            alignGroup.atlas = GetIconsAtlas();
            alignGroup.tooltip = "Align as Group - rotate around a central point";
            alignGroup.playAudioEvents = true;
            alignGroup.size = new Vector2(36, 36);
            alignGroup.normalBgSprite = "OptionBase";
            alignGroup.hoveredBgSprite = "OptionBaseHovered";
            alignGroup.pressedBgSprite = "OptionBasePressed";
            alignGroup.disabledBgSprite = "OptionBaseDisabled";
            alignGroup.normalFgSprite = "AlignGroup";
            alignGroup.eventClicked += UIAlignTools.AlignToolsClicked;

            UIAlignTools.AlignButtons.Add("MoveIt_AlignIndividualBtn", atpContainer.AddUIComponent<UIButton>());
            UIButton alignIndividual = UIAlignTools.AlignButtons["MoveIt_AlignIndividualBtn"];
            alignIndividual.name = "MoveIt_AlignIndividualBtn";
            alignIndividual.atlas = GetIconsAtlas();
            alignIndividual.tooltip = "Align In-Place - rotate selected items";
            alignIndividual.playAudioEvents = true;
            alignIndividual.size = new Vector2(36, 36);
            alignIndividual.normalBgSprite = "OptionBase";
            alignIndividual.hoveredBgSprite = "OptionBaseHovered";
            alignIndividual.pressedBgSprite = "OptionBasePressed";
            alignIndividual.disabledBgSprite = "OptionBaseDisabled";
            alignIndividual.normalFgSprite = "AlignIndividual";
            alignIndividual.eventClicked += UIAlignTools.AlignToolsClicked;

            UIAlignTools.AlignButtons.Add("MoveIt_AlignSlopeBtn", atpContainer.AddUIComponent<UIButton>());
            UIButton alignSlope = UIAlignTools.AlignButtons["MoveIt_AlignSlopeBtn"];
            alignSlope.name = "MoveIt_AlignSlopeBtn";
            alignSlope.atlas = GetIconsAtlas();
            alignSlope.tooltip = "Align Slope";
            alignSlope.playAudioEvents = true;
            alignSlope.size = new Vector2(36, 36);
            alignSlope.normalBgSprite = "OptionBase";
            alignSlope.hoveredBgSprite = "OptionBaseHovered";
            alignSlope.pressedBgSprite = "OptionBasePressed";
            alignSlope.disabledBgSprite = "OptionBaseDisabled";
            alignSlope.normalFgSprite = "AlignSlope";
            alignSlope.eventClicked += UIAlignTools.AlignToolsClicked;

            UIAlignTools.AlignButtons.Add("MoveIt_AlignTerrainHeightBtn", atpContainer.AddUIComponent<UIButton>());
            UIButton alignTerrainHeight = UIAlignTools.AlignButtons["MoveIt_AlignTerrainHeightBtn"];
            alignTerrainHeight.name = "MoveIt_AlignTerrainHeightBtn";
            alignTerrainHeight.atlas = GetIconsAtlas();
            alignTerrainHeight.tooltip = "Align Terrain Height";
            alignTerrainHeight.playAudioEvents = true;
            alignTerrainHeight.size = new Vector2(36, 36);
            alignTerrainHeight.normalBgSprite = "OptionBase";
            alignTerrainHeight.hoveredBgSprite = "OptionBaseHovered";
            alignTerrainHeight.pressedBgSprite = "OptionBasePressed";
            alignTerrainHeight.disabledBgSprite = "OptionBaseDisabled";
            alignTerrainHeight.normalFgSprite = "AlignTerrainHeight";
            alignTerrainHeight.eventClicked += UIAlignTools.AlignToolsClicked;

            UIAlignTools.AlignButtons.Add("MoveIt_AlignHeightBtn", atpContainer.AddUIComponent<UIButton>());
            UIButton alignHeight = UIAlignTools.AlignButtons["MoveIt_AlignHeightBtn"];
            alignHeight.name = "MoveIt_AlignHeightBtn";
            alignHeight.atlas = GetIconsAtlas();
            alignHeight.tooltip = "Align Height";
            alignHeight.playAudioEvents = true;
            alignHeight.size = new Vector2(36, 36);
            alignHeight.normalBgSprite = "OptionBase";
            alignHeight.hoveredBgSprite = "OptionBaseHovered";
            alignHeight.pressedBgSprite = "OptionBasePressed";
            alignHeight.disabledBgSprite = "OptionBaseDisabled";
            alignHeight.normalFgSprite = "AlignHeight";
            alignHeight.eventClicked += UIAlignTools.AlignToolsClicked;
            #endregion

            #region View Options
            viewOptions = AddUIComponent<UIPanel>();
            viewOptions.atlas = UIUtils.GetAtlas("Ingame");
            viewOptions.backgroundSprite = "InfoPanelBack";
            viewOptions.size = new Vector2(44f, 80f);

            viewOptions.absolutePosition = new Vector3(GetUIView().GetScreenResolution().x - viewOptions.width, absolutePosition.y - viewOptions.height - 8f);


            grid = viewOptions.AddUIComponent<UIMultiStateButton>();
            grid.atlas = GetIconsAtlas();
            grid.name = "MoveIt_GridView";
            grid.tooltip = "Toggle Grid";
            grid.playAudioEvents = true;

            grid.size = new Vector2(36, 36);
            grid.spritePadding = new RectOffset();

            grid.backgroundSprites[0].disabled = "OptionBaseDisabled";
            grid.backgroundSprites[0].hovered = "OptionBaseHovered";
            grid.backgroundSprites[0].normal = "OptionBase";
            grid.backgroundSprites[0].pressed = "OptionBasePressed";

            grid.backgroundSprites.AddState();
            grid.backgroundSprites[1].disabled = "OptionBaseDisabled";
            grid.backgroundSprites[1].hovered = "";
            grid.backgroundSprites[1].normal = "OptionBaseFocused";
            grid.backgroundSprites[1].pressed = "OptionBasePressed";

            grid.foregroundSprites[0].normal = "Grid";

            grid.foregroundSprites.AddState();
            grid.foregroundSprites[1].normal = "GridFocused";

            grid.relativePosition = new Vector3(4f, 4f);

            grid.activeStateIndex = 0;

            grid.eventClicked += (c, p) =>
            {
                MoveItTool.gridVisible = (grid.activeStateIndex == 1);
            };


            underground = viewOptions.AddUIComponent<UIMultiStateButton>();
            underground.atlas = UIUtils.GetAtlas("Ingame");
            underground.name = "MoveIt_UndergroundView";
            underground.tooltip = "Toogle Underground View";
            underground.playAudioEvents = true;

            underground.size = new Vector2(36, 36);
            underground.spritePadding = new RectOffset();

            underground.backgroundSprites[0].disabled = "OptionBaseDisabled";
            underground.backgroundSprites[0].hovered = "OptionBaseHovered";
            underground.backgroundSprites[0].normal = "OptionBase";
            underground.backgroundSprites[0].pressed = "OptionBasePressed";

            underground.backgroundSprites.AddState();
            underground.backgroundSprites[1].disabled = "OptionBaseDisabled";
            underground.backgroundSprites[1].hovered = "";
            underground.backgroundSprites[1].normal = "OptionBaseFocused";
            underground.backgroundSprites[1].pressed = "OptionBasePressed";

            underground.foregroundSprites[0].normal = "BulldozerOptionPipes";

            underground.foregroundSprites.AddState();
            underground.foregroundSprites[1].normal = "BulldozerOptionPipesFocused";

            underground.relativePosition = new Vector3(4f, 40f);

            underground.activeStateIndex = 0;

            underground.eventClicked += (c, p) =>
            {
                MoveItTool.tunnelVisible = (underground.activeStateIndex == 1);
            };


            if (MoveItTool.PO.Enabled)
            {
                PO_button = viewOptions.AddUIComponent<UIMultiStateButton>();
                PO_button.atlas = GetIconsAtlas();
                PO_button.name = "MoveIt_PO_button";
                PO_button.tooltip = "Toggle Procedural Objects";
                PO_button.playAudioEvents = true;

                PO_button.size = new Vector2(36, 36);
                PO_button.spritePadding = new RectOffset();

                PO_button.backgroundSprites[0].disabled = "OptionBaseDisabled";
                PO_button.backgroundSprites[0].hovered = "OptionBaseHovered";
                PO_button.backgroundSprites[0].normal = "OptionBase";
                PO_button.backgroundSprites[0].pressed = "OptionBasePressed";

                PO_button.backgroundSprites.AddState();
                PO_button.backgroundSprites[1].disabled = "OptionBaseDisabled";
                PO_button.backgroundSprites[1].hovered = "";
                PO_button.backgroundSprites[1].normal = "OptionBaseFocused";
                PO_button.backgroundSprites[1].pressed = "OptionBasePressed";

                PO_button.foregroundSprites[0].normal = "PO";

                PO_button.foregroundSprites.AddState();
                PO_button.foregroundSprites[1].normal = "POFocused";

                PO_button.relativePosition = new Vector3(4f, 76f);

                PO_button.activeStateIndex = 0;

                PO_button.eventClicked += (c, p) =>
                {
                    MoveItTool.PO.Active = (PO_button.activeStateIndex == 1);
                    if (MoveItTool.PO.Active)
                    {
                        MoveItTool.PO.ToolEnabled();
                        ActionQueue.instance.Push(new TransformAction());
                    }
                    else
                    {
                        Action.ClearPOFromSelection();
                    }
                    UIFilters.POToggled();
                };

                if (!MoveItTool.HidePO)
                {
                    viewOptions.height += 36;
                    viewOptions.absolutePosition += new Vector3(0, -36);
                }
                else
                {
                    PO_button.isVisible = false;
                }
            }

            #endregion

            MoveItTool.debugPanel = new DebugPanel();
        }

        protected override void OnVisibilityChanged()
        {
            if (isVisible)
            {
                relativePosition = new Vector2(GetUIView().GetScreenResolution().x - 448, -41);
            }
            base.OnVisibilityChanged();
        }

        public static void RefreshSnapButton()
        {
            if (instance != null && instance.m_snapping != null && MoveItTool.instance != null)
            {
                instance.m_snapping.activeStateIndex = MoveItTool.instance.snapping ? 1 : 0;
            }
        }

        public static void RefreshAlignHeightButton()
        {
            if (instance != null && instance.m_alignTools != null && MoveItTool.instance != null)
            {
                if(MoveItTool.instance.ToolState == MoveItTool.ToolStates.Aligning)
                {
                    instance.m_alignTools.normalBgSprite = "OptionBaseFocused";
                }
                else
                {
                    instance.m_alignTools.normalBgSprite = "OptionBase";
                }
            }
        }

        public static void RefreshCloneButton()
        {
            if (instance != null && instance.m_copy != null && MoveItTool.instance != null)
            {
                if (MoveItTool.instance.ToolState == MoveItTool.ToolStates.Cloning || MoveItTool.instance.ToolState == MoveItTool.ToolStates.RightDraggingClone)
                {
                    instance.m_copy.normalBgSprite = "OptionBaseFocused";
                }
                else
                {
                    instance.m_copy.normalBgSprite = "OptionBase";
                }
            }
        }

        private UITextureAtlas GetFollowTerrainAtlas()
        {

            Texture2D[] textures = 
            {
                atlas["ToggleBaseDisabled"].texture,
                atlas["ToggleBaseHovered"].texture,
                atlas["ToggleBase"].texture,
                atlas["ToggleBasePressed"].texture,
                atlas["ToggleBaseFocused"].texture
                
            };

            string[] spriteNames = new string[]
			{
				"FollowTerrain",
                "FollowTerrain_disabled"
			};

            UITextureAtlas loadedAtlas = ResourceLoader.CreateTextureAtlas("MoveIt_FollowTerrain", spriteNames, "MoveIt.Icons.");
            ResourceLoader.AddTexturesInAtlas(loadedAtlas, textures);

            return loadedAtlas;
        }

        private UITextureAtlas GetIconsAtlas()
        {

            Texture2D[] textures = 
            {
                atlas["OptionBase"].texture,
                atlas["OptionBaseHovered"].texture,
                atlas["OptionBasePressed"].texture,
                atlas["OptionBaseDisabled"].texture,
                atlas["OptionBaseFocused"].texture
            };

            string[] spriteNames = new string[]
            {
                "AlignTools",
                "AlignIndividual",
                "AlignGroup",
                "AlignRandom",
                "AlignSlope",
                "AlignSlopeA",
                "AlignSlopeB",
                "AlignHeight",
                "AlignTerrainHeight",
                "Copy",
                "Bulldoze",
                "Group",
                "Save",
                "Save_disabled",
                "Load",
                "Grid",
                "GridFocused",
                "PO",
                "POFocused"
            };

            UITextureAtlas loadedAtlas = ResourceLoader.CreateTextureAtlas("MoveIt_Icons", spriteNames, "MoveIt.Icons.");
            ResourceLoader.AddTexturesInAtlas(loadedAtlas, textures);

            return loadedAtlas;
        }
    }
}
