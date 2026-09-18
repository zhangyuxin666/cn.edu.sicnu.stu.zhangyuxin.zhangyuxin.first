package cn.edu.sicnu.stu.zhangyuxin.first;

import android.app.Activity;
import android.graphics.Color;
import android.graphics.Typeface;
import android.graphics.drawable.GradientDrawable;
import android.os.Bundle;
import android.view.Gravity;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.TextView;

/** Three-language Hello World screen, constructed entirely in Java. */
public class MainActivity extends Activity {
    private static final int[] FLAGS = {
            R.drawable.flag_china, R.drawable.flag_america, R.drawable.flag_japan
    };
    private static final String[] GREETINGS = {
            "你好，世界！", "Hello, World!", "こんにちは、世界！"
    };
    private static final String[] COUNTRIES = {"中国 · 简体中文", "United States · English", "日本 · 日本語"};
    private static final String[] DESCRIPTIONS = {"中国国旗", "美国国旗", "日本国旗"};

    private ImageView flagView;
    private TextView greetingView;
    private TextView countryView;
    private final Button[] buttons = new Button[3];

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setStatusBarColor(Color.rgb(245, 248, 252));
        getWindow().getDecorView().setSystemUiVisibility(View.SYSTEM_UI_FLAG_LIGHT_STATUS_BAR);
        buildScreen();
        showLanguage(0);
    }

    private void buildScreen() {
        int navy = Color.rgb(21, 45, 74);
        int muted = Color.rgb(90, 107, 128);
        int accent = Color.rgb(20, 111, 177);

        ScrollView scroll = new ScrollView(this);
        scroll.setFillViewport(true);
        scroll.setBackgroundColor(Color.rgb(245, 248, 252));
        LinearLayout column = new LinearLayout(this);
        column.setOrientation(LinearLayout.VERTICAL);
        column.setGravity(Gravity.CENTER_HORIZONTAL);
        int space = dp(24);
        column.setPadding(space, dp(48), space, dp(32));
        scroll.addView(column);

        TextView title = new TextView(this);
        title.setText(R.string.app_name);
        title.setTextColor(navy);
        title.setTextSize(26);
        title.setTypeface(Typeface.DEFAULT, Typeface.BOLD);
        title.setGravity(Gravity.CENTER);
        column.addView(title, new LinearLayout.LayoutParams(-1, -2));

        TextView hint = new TextView(this);
        hint.setText("点击按钮，切换问候语与国旗");
        hint.setTextColor(muted);
        hint.setTextSize(15);
        hint.setGravity(Gravity.CENTER);
        LinearLayout.LayoutParams hintParams = new LinearLayout.LayoutParams(-1, -2);
        hintParams.topMargin = dp(10);
        column.addView(hint, hintParams);

        LinearLayout card = new LinearLayout(this);
        card.setOrientation(LinearLayout.VERTICAL);
        card.setGravity(Gravity.CENTER);
        card.setPadding(dp(20), dp(28), dp(20), dp(28));
        GradientDrawable cardBg = new GradientDrawable();
        cardBg.setColor(Color.WHITE);
        cardBg.setCornerRadius(dp(20));
        card.setBackground(cardBg);
        card.setElevation(dp(4));
        LinearLayout.LayoutParams cardParams = new LinearLayout.LayoutParams(-1, -2);
        cardParams.topMargin = dp(35);
        column.addView(card, cardParams);

        flagView = new ImageView(this);
        flagView.setScaleType(ImageView.ScaleType.FIT_CENTER);
        LinearLayout.LayoutParams flagParams = new LinearLayout.LayoutParams(dp(210), dp(130));
        card.addView(flagView, flagParams);

        countryView = new TextView(this);
        countryView.setTextColor(muted);
        countryView.setTextSize(15);
        countryView.setGravity(Gravity.CENTER);
        LinearLayout.LayoutParams countryParams = new LinearLayout.LayoutParams(-1, -2);
        countryParams.topMargin = dp(18);
        card.addView(countryView, countryParams);

        greetingView = new TextView(this);
        greetingView.setTextColor(navy);
        greetingView.setTextSize(29);
        greetingView.setTypeface(Typeface.DEFAULT, Typeface.BOLD);
        greetingView.setGravity(Gravity.CENTER);
        greetingView.setMinHeight(dp(64));
        LinearLayout.LayoutParams greetingParams = new LinearLayout.LayoutParams(-1, -2);
        greetingParams.topMargin = dp(8);
        card.addView(greetingView, greetingParams);

        LinearLayout row = new LinearLayout(this);
        row.setOrientation(LinearLayout.HORIZONTAL);
        LinearLayout.LayoutParams rowParams = new LinearLayout.LayoutParams(-1, -2);
        rowParams.topMargin = dp(32);
        column.addView(row, rowParams);
        String[] labels = {"中文", "English", "日本語"};
        for (int i = 0; i < labels.length; i++) {
            final int index = i;
            Button button = new Button(this);
            button.setText(labels[i]);
            button.setTextSize(14);
            button.setAllCaps(false);
            button.setOnClickListener(v -> showLanguage(index));
            LinearLayout.LayoutParams buttonParams = new LinearLayout.LayoutParams(0, dp(54), 1);
            if (i > 0) buttonParams.leftMargin = dp(8);
            row.addView(button, buttonParams);
            buttons[i] = button;
        }
        setContentView(scroll);
    }

    private void showLanguage(int index) {
        flagView.setImageResource(FLAGS[index]);
        flagView.setContentDescription(DESCRIPTIONS[index]);
        greetingView.setText(GREETINGS[index]);
        countryView.setText(COUNTRIES[index]);
        for (int i = 0; i < buttons.length; i++) {
            buttons[i].setEnabled(i != index);
            buttons[i].setAlpha(i == index ? 0.6f : 1f);
        }
    }

    private int dp(float value) {
        return Math.round(value * getResources().getDisplayMetrics().density);
    }
}
