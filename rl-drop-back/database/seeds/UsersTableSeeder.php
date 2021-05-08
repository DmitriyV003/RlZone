<?php

use Illuminate\Database\Seeder;

class UsersTableSeeder extends Seeder
{
  /**
   * Run the database seeds.
   *
   * @return void
   */
  public function run()
  {
    factory(\App\User::class, 20)->create()->each(function ($user) {
      $user->passwordSecurity()
        ->save(factory(\App\PasswordSecurity::class)->make());
    });
  }
}
