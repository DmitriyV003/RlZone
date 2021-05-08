<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateFaqsTable extends Migration
{
  /**
   * Run the migrations.
   *
   * @return void
   */
  public function up()
  {
    Schema::create('faqs', function (Blueprint $table) {
      $table->id();
//      $table->string('title');
//      $table->text('text');
//      $table->string('category');
      $table->text('text_ru');
      $table->text('text_en');
      $table->string('title_ru');
      $table->string('title_en');
      $table->string('category_ru');
      $table->string('category_en');
      $table->timestamps();
    });
  }

  /**
   * Reverse the migrations.
   *
   * @return void
   */
  public function down()
  {
    Schema::dropIfExists('faqs');
  }
}
