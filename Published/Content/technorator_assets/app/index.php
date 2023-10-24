<?php require_once dirname(__FILE__) . '/includes/commons/_header.php'; ?>
	
	<div class="welcome-container" style="background-image: url('assets/images/backgrounds/welcome-bg.jpg');">
		<div class="welcome-content">
			<div class="container">
				<div class="welcome-inner">

					<section class="welcome-title-container">
						<div class="welcome-title-inner">
							<h1>
								Rates From Over 30 Car Rental Companies <br/>
								<span class="fa fa-plus text-success"></span>
								 $5 Amazon E-Gift Card When You Book!
							</h1>
						</div>
					</section>

					<section class="search-container main-search-container">
						<div class="search-wrapper">
							<div class="search-inner">
								<form action="#" class="global-form">
									<div class="form-title">
										<h3><span class="fa fa-search"></span> Search for a car rental</h3>
									</div>
									<div class="locations-container" id="cnt_location_search">
										<div class="locations-inner">
											<div class="form-group pickup-group">
												<div class="input-group">
													<input type="text" name="pickup_location" id="pickup_location" class="form-control" placeholder="Pickup Location"/>
													<span class="group-addon"><span class="fa fa-map-marker"></span></span>
												</div>
											</div>
											<div class="form-group dropoff-group">
												<div class="input-group">
													<input type="text" name="dropoff_location" id="dropoff_location" class="form-control" placeholder="Dropoff Location"/>
													<span class="group-addon"><span class="fa fa-map-marker"></span></span>
												</div>
											</div>
										</div>
									</div>
									<div class="row row-10 row-xxs">
										<div class="col col-xs-6">
											<div class="form-group">
												<div class="input-group">
													<input type="text" name="pickup_date" id="pickup_date" class="form-control" placeholder="Pickup Date"/>
													<span class="group-addon"><span class="fa fa-calendar"></span></span>
												</div>
											</div>
										</div>
										<div class="col col-xs-6">
											<div class="form-group">
												<div class="input-group">
													<select name="pickup_location_time" id="pickup_location_time" class="form-control">
														<option>10:00 AM</option>
														<option>11:00 AM</option>
														<option>12:00 PM</option>
													</select>
													<span class="group-addon"><span class="fa fa-clock-o"></span></span>
												</div>
											</div>
										</div>
										<div class="col col-xs-6">
											<div class="form-group">
												<div class="input-group">
													<input type="text" name="dropoff_date" id="dropoff_date" class="form-control" placeholder="Dropoff Date"/>
													<span class="group-addon"><span class="fa fa-calendar"></span></span>
												</div>
											</div>
										</div>
										<div class="col col-xs-6">
											<div class="form-group">
												<div class="input-group">
													<select name="dropoff_location_time" id="dropoff_location_time" class="form-control">
														<option>10:00 AM</option>
														<option>11:00 AM</option>
														<option>12:00 PM</option>
													</select>
													<span class="group-addon"><span class="fa fa-clock-o"></span></span>
												</div>
											</div>
										</div>
									</div>
									<div class="form-actions">
										<div class="actions-inner">
											<div class="location-toggler">
												<label class="btn-location-toggler form-checkbox-label" id="btn_location_toggler">
													<input type="checkbox" name="dropoff_location_checkbox" class="dropoff_location_checkbox"/>
													<span class="btn-text">Return car to different location</span>
												</label>
											</div>
											<div class="btn-submit">
												<div class="btn-inner">
													<div class="btn-wrapper">
														<button class="btn btn-primary btn-block">Search</button>
													</div>
												</div>
											</div>
										</div>
									</div>
								</form>
							</div>
						</div>
					</section>

				</div>
			</div>
		</div>
	</div>

	<!-- Scoped Scripts -->
	<script type="text/javascript">
		$(function()
		{
			$(window).on('load resize', function()
			{
				w_width = $(window).width();
				w_height = $(window).height();

				setTimeout(function()
				{
					var height = $('#cnt_body').height();

					if(w_width > 767)
					{
						$('#cnt_content').height(height + 'px');
					}
				}, 50);
			});
		});
	</script>

<?php require_once dirname(__FILE__) . '/includes/commons/_footer.php'; ?>