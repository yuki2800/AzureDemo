# Inventory Design System

**社内向けBlazor在庫管理システム用デザインシステム**

コーポレートカラー（紺/黒系）を参考にした統一的なUIコンポーネントライブラリです。

---

## 📁 プロジェクト構成

```
InventoryDesignSystem/
├── Components/                 # Razorコンポーネント
│   ├── Button.razor            # ボタンコンポーネント
│   ├── TextInput.razor         # テキスト入力
│   ├── NumberInput.razor       # 数値入力（在庫数用）
│   ├── SelectDropdown.razor    # ドロップダウン
│   ├── Badge.razor             # ステータスバッジ
│   └── DataTable.razor         # データテーブル
├── wwwroot/
│   └── css/
│       ├── variables.css       # デザイントークン定義
│       └── global.css          # グローバルスタイル
├── _Imports.razor              # グローバルインポート
└── InventoryDesignSystem.csproj
```

---

## 🎨 デザイントークン

### カラーパレット

| 用途 | 変数 | 色 |
|------|------|-----|
| **プライマリ** | `--color-primary` | #00A99D（エメラルドグリーン） |
| **プライマリ（濃い）** | `--color-primary-dark` | #00817A |
| **プライマリ（明るい）** | `--color-primary-light` | #5FCFC4 |
| **成功** | `--color-success` | #107C10（緑） |
| **警告** | `--color-warning` | #FFB900（ゴールド） |
| **エラー** | `--color-error` | #DA3B01（オレンジレッド） |

### タイポグラフィ

- **フォント**: `Hiragino Sans`, `Yu Gothic`, `Meiryo`, システムフォント
- **デフォルトサイズ**: 14px
- **見出しサイズ**: 20px～28px
- **ウェイト**: 300～700

### スペーシング

- `--spacing-xs`: 4px
- `--spacing-sm`: 8px
- `--spacing-md`: 12px
- `--spacing-lg`: 16px
- `--spacing-xl`: 24px

---

## 🧩 コンポーネント使用ガイド

### 1. Button（ボタン）

```razor
<Button Text="保存" Variant="primary" />
<Button Text="キャンセル" Variant="secondary" />
<Button Text="削除" Variant="danger" IsDisabled="false" />
```

**パラメータ:**
- `Text`: ボタンテキスト
- `Variant`: `primary` / `secondary` / `danger` （デフォルト: primary）
- `IsDisabled`: 無効化フラグ
- `Icon`: アイコン（オプション）
- `OnClick`: クリックイベント

---

### 2. TextInput（テキスト入力）

```razor
<TextInput Label="商品名" 
           Placeholder="商品名を入力" 
           Value="@productName"
           ValueChanged="@((v) => productName = v)" />
```

**パラメータ:**
- `Label`: ラベルテキスト
- `Placeholder`: プレースホルダ
- `Value`: 入力値
- `ValueChanged`: 値変更イベント
- `IsDisabled`: 無効化フラグ
- `HelperText`: 補助テキスト

---

### 3. NumberInput（数値入力）

```razor
<NumberInput Label="在庫数" 
             Value="@quantity"
             ValueChanged="@((v) => quantity = v)"
             Min="0"
             Max="9999"
             HelperText="0以上の数値を入力してください" />
```

**パラメータ:**
- `Label`: ラベルテキスト
- `Value`: 入力値（int）
- `ValueChanged`: 値変更イベント
- `Min`: 最小値（デフォルト: 0）
- `Max`: 最大値（オプション）
- `Step`: 増減単位（デフォルト: 1）

---

### 4. SelectDropdown（ドロップダウン）

```razor
<SelectDropdown Label="カテゴリ"
                SelectedValue="@selectedCategory"
                SelectedValueChanged="@((v) => selectedCategory = v)"
                Options="@categoryOptions"
                Placeholder="選択してください" />

@code {
    private string selectedCategory = "";
    
    private List<SelectDropdown.SelectOption> categoryOptions = new()
    {
        new SelectDropdown.SelectOption { Label = "金属", Value = "metal" },
        new SelectDropdown.SelectOption { Label = "電子部品", Value = "electronic" },
        new SelectDropdown.SelectOption { Label = "その他", Value = "other" }
    };
}
```

**パラメータ:**
- `Label`: ラベルテキスト
- `SelectedValue`: 選択値
- `SelectedValueChanged`: 値変更イベント
- `Options`: `SelectOption` リスト（Label/Value）
- `Placeholder`: プレースホルダ

---

### 5. Badge（ステータスバッジ）

```razor
<!-- 在庫充分 -->
<Badge Text="在庫充分" Variant="success" />

<!-- 在庫少 -->
<Badge Text="在庫少" Variant="warning" />

<!-- 在庫切れ -->
<Badge Text="在庫切れ" Variant="error" />

<!-- 情報 -->
<Badge Text="新商品" Variant="info" />
```

**パラメータ:**
- `Text`: バッジテキスト
- `Variant`: `success` / `warning` / `error` / `info` / `primary` / `neutral`
- `Icon`: アイコン（オプション）
- `Tooltip`: ホバー時のツールチップ

---

### 6. DataTable（データテーブル）

```razor
<DataTable Title="在庫一覧"
           Columns="@columns"
           Rows="@inventoryData"
           ShowActions="true"
           OnRowAction="@HandleRowAction" />

@code {
    private List<DataTable.DataColumn> columns = new()
    {
        new DataTable.DataColumn { Header = "商品ID", FieldName = "ProductId", Width = 15 },
        new DataTable.DataColumn { Header = "商品名", FieldName = "ProductName", Width = 30 },
        new DataTable.DataColumn { Header = "在庫数", FieldName = "Quantity", Width = 15, TextAlign = "right" },
        new DataTable.DataColumn { Header = "ステータス", FieldName = "Status", Width = 20 }
    };

    private List<Dictionary<string, object>> inventoryData = new()
    {
        new Dictionary<string, object> 
        { 
            { "ProductId", "001" },
            { "ProductName", "銅板A" },
            { "Quantity", 150 },
            { "Status", "在庫充分" }
        }
    };

    private async Task HandleRowAction(Dictionary<string, object> row)
    {
        // 編集処理
        var productId = row["ProductId"];
    }
}
```

**パラメータ:**
- `Title`: テーブルタイトル
- `Columns`: `DataColumn` リスト（Header/FieldName/Width/TextAlign）
- `Rows`: `Dictionary<string, object>` リスト
- `ShowActions`: アクションボタン表示フラグ
- `OnRowAction`: 行アクションイベント

---

## 🎯 在庫管理システムへの組み込み

### 親プロジェクトの設定

1. **プロジェクト参照を追加:**
```xml
<ItemGroup>
    <ProjectReference Include="..\InventoryDesignSystem\InventoryDesignSystem.csproj" />
</ItemGroup>
```

2. **レイアウトでCSSをリンク:**
```html
<!-- App.razor or Layout.razor -->
<link href="_framework/bundle.scp.css" rel="stylesheet" />
<link href="InventoryDesignSystem/css/variables.css" rel="stylesheet" />
<link href="InventoryDesignSystem/css/global.css" rel="stylesheet" />
```

3. **コンポーネントを使用:**
```razor
@using InventoryDesignSystem.Components

<Button Text="新規登録" Variant="primary" />
<TextInput Label="商品検索" />
<DataTable Columns="@..." Rows="@..." />
```

---

## 🌈 カラーユーティリティクラス

```razor
<!-- テキスト色 -->
<p class="text-muted">補助テキスト</p>
<p class="text-error">エラーメッセージ</p>
<p class="text-success">成功メッセージ</p>
<p class="text-warning">警告メッセージ</p>

<!-- スペーシング -->
<div class="mb-lg">マージン下（大）</div>
<div class="p-md">パディング（中）</div>

<!-- テキストサイズ -->
<p class="text-small">小さいテキスト</p>
<p class="text-large">大きいテキスト</p>
```

---

## 📋 使用可能なユーティリティクラス

| クラス | 効果 |
|--------|------|
| `.btn-primary` | プライマリボタン |
| `.btn-secondary` | セカンダリボタン |
| `.btn-danger` | ダンジャーボタン |
| `.badge-success` | 成功バッジ |
| `.badge-warning` | 警告バッジ |
| `.badge-error` | エラーバッジ |
| `.text-muted` | グレーテキスト |
| `.text-error` | 赤テキスト |
| `.text-success` | 緑テキスト |
| `.mb-sm`, `.mb-md`, `.mb-lg` | マージン下 |
| `.mt-sm`, `.mt-md`, `.mt-lg` | マージン上 |
| `.p-sm`, `.p-md`, `.p-lg` | パディング |

---

## 🔧 カスタマイズ方法

### 色を変更する場合

`wwwroot/css/variables.css` のCSS変数を編集してください：

```css
:root {
  --color-primary: #003366;      /* ← ここを変更 */
  --color-success: #2E7D32;      /* ← ここを変更 */
}
```

### フォントを変更する場合

```css
:root {
  --font-family-sans: 'Arial', sans-serif;  /* ← ここを変更 */
}
```

---

## 📦 バージョン情報

- **対応フレームワーク**: .NET 8.0
- **Blazor**: Razor Component Library
- **作成日**: 2026年9月

---

## 📝 注意事項

- 社内スタッフ向けのシンプルな設計です
- アクセシビリティの完全対応は含まれていません
- 大規模データ（1000行以上）の表示には、仮想スクロール機能の追加を検討してください
