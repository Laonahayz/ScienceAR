<h1 style="font-size: 28px;">ScienceAR - AR皂化實驗室</h1>
<p>ScienceAR 是一款執行在 Unity 與 Vuforia 環境下的化學教育實驗系統。</p>
<p>本系統展示了 AR (擴增實境) 技術在教學互動與流程設計上的應用，支援即時提示、行為紀錄與錯題導向回饋等教學輔助機制。</p>
<h2 style="font-size: 20px;">系統構成</h2>
<p>整個系統分為以下幾個階段：</p>
<h3 style="font-size: 16px;">1. 實驗操作階段</h3>
<ul>
  <li>利用 Vuforia 圖卡識別，學生可以操作處理虛擬化學實驗步驟</li>
  <li>與 AR 物件進行互動，包含拖曳、加入藥品、攪拌等操作</li>
  <li>系統會根據學生行為自動偵測操作狀況，若出現停滯或錯誤次數過高，即時提供提示與輔助</li>
  <li>提示依據學生表現分層顯示（普通提示/詳細提示/高階問題），實現適應性教學邏輯</li>
</ul>
<h3 style="font-size: 16px;">2. 測驗階段</h3>
<ul>
  <li>實驗操作後，學生需回答與實驗背景知識相關的選擇題</li>
  <li>系統根據作答結果記錄錯題題號與對應實驗步驟</li>
  <li>若有錯誤，系統會引導學生回到該步驟重新操作，加強弱項學習</li>
</ul>
<h3 style="font-size: 16px;">3. 錯題解說與重試階段</h3>
<ul>
  <li>所有題目作答完畢後，系統會顯示詳細解說幫助學生理解錯誤原因</li>
  <li>學生需再次作答第一次答錯的題目，深化知識掌握</li>
</ul>
<h3 style="font-size: 16px;">4. 學習行為紀錄</h3>
<ul>
  <li>系統會於實驗全程中即時記錄學生操作行為：如錯誤類型、步驟完成時間、是否停頓過久等資訊</li>
  <li>使用 BehaviorLogger 將資料輸出為 .txt 檔案並儲存在裝置本地</li>
  <li>可支援匯出操作紀錄檔至外部裝置，供教師後續分析學習歷程</li>
</ul>
<h3 style="font-size: 16px;">5. 問卷連結功能</h3>
<ul>
  <li>完成所有流程後，按鈕可導引至外部線上問卷（如 Google 表單），方便收集使用者回饋與後測結果</li>
</ul>

<h2 style="font-size: 20px;">使用技術</h2>
<ul>
  <li>Unity：主要開發平台</li>
  <li>Vuforia Engine：圖卡辨識與物件追蹤互動</li>
  <li>C#：撰寫互動邏輯、提示控制與資料紀錄</li>
  <li>Lean Touch (觸控互動)：支援手勢操作與物件旋轉、拖曳</li>
</ul>

<h2 style="font-size: 20px;">核心功能模組與腳本對應</h2>
<ul>
<li>實驗提示系統（Hint_Manager.cs, Hint_Level.cs）：依學生行為狀況提供不同層級提示</li>
<li>互動動畫控制（Beaker_Anim.cs）：控制燒杯加熱、溶液變化等動畫流程</li>
<li>行為紀錄系統（BehaviorLogger.cs）：記錄學生操作錯誤與互動時間</li>
<li>答題測驗與回饋引導（QuestionsM2.cs, PersistenceManager.cs）：紀錄錯題並引導重做</li>
<li>連結問卷功能（Buttons_Control.cs）：流程結束後導引至 Google 表單</li>
</ul>

<h2 style="font-size: 20px;">設計目標</h2>
<p>本系統旨在確保學生能夠有效學會化學實驗相關步驟與知識，透過圖卡引導、即時回饋、測驗與重試設計，實踐一套完整的 AR 教學輔助架構，並可做為自學平台、翻轉教室或實驗教學前置預習之用。</p>

<h2 style="font-size: 20px;">系統畫面展示</h2>
